using System;
using System.Collections.Generic;
using Frosty.Sdk.IO;
using FrostyHavokPlugin.CommonTypes;
using FrostyHavokPlugin.Interfaces;
using FrostyHavokPlugin.Utils;
using OpenTK.Mathematics;
using Half = System.Half;

namespace FrostyHavokPlugin;

public class PackFileDeserializer
{
    public HKXHeader Header => m_header;

    private HKXClassNames m_classnames;
    private HKXSection m_classSection;
    private HKXSection m_dataSection;

    private Dictionary<uint, IHavokObject> m_deserializedObjects;
    private HKXHeader m_header;
    private HKXSection m_typeSection;

    private IHavokObject ConstructVirtualClass(DataStream br, uint offset)
    {
        if (m_deserializedObjects.ContainsKey(offset))
        {
            return m_deserializedObjects[offset];
        }

        VirtualFixup? fixup = m_dataSection._virtualMap[offset];
        string? hkClassName = m_classnames.OffsetClassNamesMap[fixup.Dst].ClassName;

        Type? hkClass = System.Type.GetType($"hk.{hkClassName}");
        if (hkClass is null)
        {
            throw new Exception($"Havok class type '{hkClassName}' not found!");
        }

        IHavokObject? ret = (IHavokObject)Activator.CreateInstance(hkClass)!;
        if (ret is null)
        {
            throw new Exception($"Failed to Activator.CreateInstance({hkClass})");
        }

        br.StepIn(offset);
        ret.Read(this, br);
        br.StepOut();

        m_deserializedObjects.Add(offset, ret);
        return ret;
    }

    public void DeserializePartially(DataStream br, uint fixupTableOffset)
    {
        long startOffset = br.Position;
        m_header = new HKXHeader(br);

        m_classSection = new HKXSection(br, m_header.ContentsVersionString, startOffset, fixupTableOffset) { SectionID = 0 };
        m_typeSection = new HKXSection(br, m_header.ContentsVersionString, startOffset, fixupTableOffset) { SectionID = 1 };
        m_dataSection = new HKXSection(br, m_header.ContentsVersionString, startOffset, fixupTableOffset) { SectionID = 2 };

        // Process the class names
        m_classnames = m_classSection.ReadClassnames();
    }

    public IHavokObject Deserialize(DataStream br, uint fixupTableOffset)
    {
        DeserializePartially(br, fixupTableOffset);

        // Deserialize the objects
        m_deserializedObjects = new Dictionary<uint, IHavokObject>();
        return ConstructVirtualClass(m_dataSection.SectionData, 0);
    }

    #region Read methods

    public ulong ReadUSize(DataStream br)
    {
        if (m_header.PointerSize == 8)
        {
            return br.ReadUInt64();
        }

        return br.ReadUInt32();
    }

    public ulong AssertUSize(DataStream br, params ulong[] options)
    {
        return br.AssertValue(ReadUSize(br), m_header.PointerSize == 8 ? "USize64" : "USize32", "0x{0:X}", options);
    }

    private void PadToPointerSizeIfPaddingOption(DataStream br)
    {
        if (m_header.PaddingOption == 1)
        {
            br.Pad(m_header.PointerSize);
        }
    }

    public void ReadEmptyPointer(DataStream br)
    {
        PadToPointerSizeIfPaddingOption(br);
        AssertUSize(br, 0);
    }

    public void ReadEmptyArray(DataStream br)
    {
        ReadEmptyPointer(br);
        uint size = br.ReadUInt32();
        br.AssertUInt32(size | ((uint)0x80 << 24));
    }

    private T ReadPointerBase<T, F>(Func<DataStream, F, T> func, DataStream br) where F : IFixup, new()
    {
        Dictionary<uint, F> map;
        if (typeof(F) == typeof(LocalFixup))
        {
            map = (Dictionary<uint, F>)(object)m_dataSection._localMap;
        }
        else if (typeof(F) == typeof(GlobalFixup))
        {
            map = (Dictionary<uint, F>)(object)m_dataSection._localMap;
        }
        else
        {
            throw new Exception();
        }

        PadToPointerSizeIfPaddingOption(br);

        uint key = (uint)br.Position;

        AssertUSize(br, 0);

        if (!map.ContainsKey(key))
        {
            // If the read type is an array, continue like normal
            Type? oType = typeof(T);
            if (oType.IsGenericType && oType.GetGenericTypeDefinition() == typeof(List<>))
            {
                return func(br, new F());
            }

            return default;
        }

        F? f = map[key];
        return func(br, f);
    }

    private List<T> ReadArrayBase<T>(Func<DataStream, T> func, DataStream br)
    {
        return ReadPointerBase((DataStream _br, LocalFixup f) =>
        {
            int size = _br.ReadInt32();
            _br.AssertInt32(size | (0x80 << 24)); // Capacity and flags

            List<T>? res = new(size);

            if (size <= 0)
            {
                return res;
            }

            _br.StepIn(f.Dst);
            for (int i = 0; i < size; i++)
            {
                res.Add(func(_br));
            }

            _br.StepOut();

            return res;
        }, br);
    }

    private List<T> ReadRelArrayBase<T>(Func<DataStream, T> func, DataStream br)
    {
        ushort size = br.ReadUInt16();
        ushort offset = br.ReadUInt16();

        List<T>? res = new(size);

        br.StepIn(br.Position - 4 + offset);
        for (int i = 0; i < size; i++)
        {
            res.Add(func(br));
        }

        br.StepOut();

        return res;
    }

    public List<T> ReadClassArray<T>(DataStream br) where T : IHavokObject, new()
    {
        return ReadArrayBase(_br =>
        {
            T? cls = new();
            cls.Read(this, _br);
            return cls;
        }, br);
    }

    public List<T> ReadClassRelArray<T>(DataStream br) where T : IHavokObject, new()
    {
        return ReadRelArrayBase(_br =>
        {
            T? cls = new();
            cls.Read(this, _br);
            return cls;
        }, br);
    }

    public T ReadClassPointer<T>(DataStream br) where T : IHavokObject
    {
        PadToPointerSizeIfPaddingOption(br);

        uint key = (uint)br.Position;

        // Consume pointer
        AssertUSize(br, 0);

        // Do a global fixup lookup
        if (!m_dataSection._globalMap.ContainsKey(key))
        {
            return default;
        }

        GlobalFixup? f = m_dataSection._globalMap[key];
        IHavokObject? klass = ConstructVirtualClass(br, f.Dst);

        if (klass.GetType().IsAssignableTo(typeof(T)))
        {
            return (T)klass;
        }

        throw new Exception($"Could not convert '{typeof(T)}' to '{klass.GetType()}'. Is source malformed?");
    }

    public List<T> ReadClassPointerArray<T>(DataStream br) where T : IHavokObject
    {
        return ReadArrayBase(ReadClassPointer<T>, br);
    }

    public List<T> ReadClassPointerRelArray<T>(DataStream br) where T : IHavokObject
    {
        return ReadRelArrayBase(ReadClassPointer<T>, br);
    }

    public string ReadStringPointer(DataStream br)
    {
        PadToPointerSizeIfPaddingOption(br);

        uint key = (uint)br.Position;

        // Consume pointer
        AssertUSize(br, 0);

        // Do a local fixup lookup
        if (!m_dataSection._localMap.ContainsKey(key))
        {
            return string.Empty;
        }

        LocalFixup? f = m_dataSection._localMap[key];
        br.StepIn(f.Dst);
        string? ret = br.ReadNullTerminatedString();
        br.StepOut();
        return ret.Trim();
    }

    public List<string> ReadStringPointerArray(DataStream br)
    {
        return ReadArrayBase(ReadStringPointer, br);
    }

    public List<string> ReadStringPointerRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadStringPointer, br);
    }

    public string ReadCString(DataStream br)
    {
        PadToPointerSizeIfPaddingOption(br);

        uint key = (uint)br.Position;

        // Consume pointer
        AssertUSize(br, 0);

        // Do a local fixup lookup
        if (!m_dataSection._localMap.ContainsKey(key))
        {
            return null;
        }

        LocalFixup? f = m_dataSection._localMap[key];
        br.StepIn(f.Dst);
        string? ret = br.ReadNullTerminatedString();
        br.StepOut();
        if (ret == "")
        {
            return null;
        }

        return ret;
    }

    public List<string> ReadCStringArray(DataStream br)
    {
        return ReadArrayBase(ReadCString, br);
    }

    public List<string> ReadCStringRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadCString, br);
    }

    public byte ReadByte(DataStream br)
    {
        return br.ReadByte();
    }

    public List<byte> ReadByteArray(DataStream br)
    {
        return ReadArrayBase(ReadByte, br);
    }

    public List<byte> ReadByteRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadByte, br);
    }

    public sbyte ReadSByte(DataStream br)
    {
        return br.ReadSByte();
    }

    public List<sbyte> ReadSByteArray(DataStream br)
    {
        return ReadArrayBase(ReadSByte, br);
    }

    public List<sbyte> ReadSByteRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadSByte, br);
    }

    public ushort ReadUInt16(DataStream br)
    {
        return br.ReadUInt16();
    }

    public List<ushort> ReadUInt16Array(DataStream br)
    {
        return ReadArrayBase(ReadUInt16, br);
    }

    public List<ushort> ReadUInt16RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadUInt16, br);
    }

    public short ReadInt16(DataStream br)
    {
        return br.ReadInt16();
    }

    public List<short> ReadInt16Array(DataStream br)
    {
        return ReadArrayBase(ReadInt16, br);
    }

    public List<short> ReadInt16RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadInt16, br);
    }

    public uint ReadUInt32(DataStream br)
    {
        return br.ReadUInt32();
    }

    public List<uint> ReadUInt32Array(DataStream br)
    {
        return ReadArrayBase(ReadUInt32, br);
    }

    public List<uint> ReadUInt32RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadUInt32, br);
    }

    public int ReadInt32(DataStream br)
    {
        return br.ReadInt32();
    }

    public List<int> ReadInt32Array(DataStream br)
    {
        return ReadArrayBase(ReadInt32, br);
    }

    public List<int> ReadInt32RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadInt32, br);
    }

    public ulong ReadUInt64(DataStream br)
    {
        return br.ReadUInt64();
    }

    public List<ulong> ReadUInt64Array(DataStream br)
    {
        return ReadArrayBase(ReadUInt64, br);
    }

    public List<ulong> ReadUInt64RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadUInt64, br);
    }

    public long ReadInt64(DataStream br)
    {
        return br.ReadInt64();
    }

    public List<long> ReadInt64Array(DataStream br)
    {
        return ReadArrayBase(ReadInt64, br);
    }

    public List<long> ReadInt64RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadInt64, br);
    }

    public Half ReadHalf(DataStream br)
    {
        return br.ReadHalf();
    }

    public List<Half> ReadHalfArray(DataStream br)
    {
        return ReadArrayBase(ReadHalf, br);
    }

    public List<Half> ReadHalfRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadHalf, br);
    }

    public float ReadSingle(DataStream br)
    {
        return br.ReadSingle();
    }

    public List<float> ReadSingleArray(DataStream br)
    {
        return ReadArrayBase(ReadSingle, br);
    }

    public List<float> ReadSingleRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadSingle, br);
    }

    public bool ReadBoolean(DataStream br)
    {
        return br.ReadBoolean();
    }

    public List<bool> ReadBooleanArray(DataStream br)
    {
        return ReadArrayBase(ReadBoolean, br);
    }

    public List<bool> ReadBooleanRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadBoolean, br);
    }

    public Vector4 ReadVector4(DataStream br)
    {
        return br.ReadVector4();
    }

    public List<Vector4> ReadVector4Array(DataStream br)
    {
        return ReadArrayBase(ReadVector4, br);
    }

    public List<Vector4> ReadVector4RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadVector4, br);
    }

    public Matrix3x4 ReadMatrix3(DataStream br)
    {
        Matrix3x4 mat3 = new(
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        return mat3;
    }

    public List<Matrix3x4> ReadMatrix3Array(DataStream br)
    {
        return ReadArrayBase(ReadMatrix3, br);
    }

    public List<Matrix3x4> ReadMatrix3RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadMatrix3, br);
    }

    public Matrix4 ReadMatrix4(DataStream br)
    {
        return new Matrix4(
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
    }

    public List<Matrix4> ReadMatrix4Array(DataStream br)
    {
        return ReadArrayBase(ReadMatrix4, br);
    }

    public List<Matrix4> ReadMatrix4RelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadMatrix4, br);
    }

    public Matrix4 ReadTransform(DataStream br)
    {
        Matrix4 transform = new(
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

        return transform;
    }

    public List<Matrix4> ReadTransformArray(DataStream br)
    {
        return ReadArrayBase(ReadTransform, br);
    }

    public List<Matrix4> ReadTransformRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadTransform, br);
    }

    public Matrix3x4 ReadQSTransform(DataStream br)
    {
        Matrix3x4 qsTransform = new(
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(),
            br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

        return qsTransform;
    }

    public List<Matrix3x4> ReadQSTransformArray(DataStream br)
    {
        return ReadArrayBase(ReadQSTransform, br);
    }

    public List<Matrix3x4> ReadQSTransformRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadQSTransform, br);
    }

    public Quaternion ReadQuaternion(DataStream br)
    {
        return new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
    }

    public List<Quaternion> ReadQuaternionArray(DataStream br)
    {
        return ReadArrayBase(ReadQuaternion, br);
    }

    public List<Quaternion> ReadQuaternionRelArray(DataStream br)
    {
        return ReadRelArrayBase(ReadQuaternion, br);
    }


    #region C Style Array
    private T[] ReadCStyleArrayBase<T>(Func<DataStream, T> func, DataStream br, short length)
    {
        T[]? res = new T[length];
        for (int i = 0; i < length; i++)
        {
            res[i] = (func(br));
        }
        return res;
    }

    public bool[] ReadBooleanCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadBoolean, br, length);
    }

    public byte[] ReadByteCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadByte, br, length);
    }

    public sbyte[] ReadSByteCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadSByte, br, length);
    }

    public short[] ReadInt16CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadInt16, br, length);
    }

    public ushort[] ReadUInt16CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadUInt16, br, length);
    }
    public int[] ReadInt32CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadInt32, br, length);
    }
    public uint[] ReadUInt32CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadUInt32, br, length);
    }

    public long[] ReadInt64CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadInt64, br, length);
    }

    public ulong[] ReadUInt64CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadUInt64, br, length);
    }

    public Half[] ReadHalfCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadHalf, br, length);
    }

    public float[] ReadSingleCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadSingle, br, length);
    }

    public Vector4[] ReadVector4CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadVector4, br, length);
    }

    public Quaternion[] ReadQuaternionCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadQuaternion, br, length);
    }
    public Matrix3x4[] ReadMatrix3CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadMatrix3, br, length);
    }

    public Matrix3x4[] ReadRotationCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadMatrix3, br, length);
    }

    public Matrix3x4[] ReadQSTransformCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadQSTransform, br, length);
    }

    public Matrix4[] ReadMatrix4CStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadMatrix4, br, length);
    }

    public Matrix4[] ReadTransformCStyleArray(DataStream br, short length)
    {
        return ReadCStyleArrayBase(ReadTransform, br, length);
    }

    public T[] ReadClassPointerCStyleArray<T>(DataStream br, short length) where T : IHavokObject, new()
    {
        return ReadCStyleArrayBase(ReadClassPointer<T>, br, length);
    }

    public void ReadEmptyPointerCStyleArray(DataStream br, short length)
    {
        for (int i = 0; i < length; i++)
        {
            ReadEmptyPointer(br);
        }
    }

    public T[] ReadStructCStyleArray<T>(DataStream br, short length) where T : IHavokObject, new()
    {
        T[]? res = new T[length];
        for (int i = 0; i < length; i++)
        {
            T? s = new();
            s.Read(this, br);
            res[i] = s;
        }
        return res;
    }

    #endregion

    #endregion
}