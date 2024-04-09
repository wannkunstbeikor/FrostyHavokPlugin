using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin.CommonTypes;
using FrostyHavokPlugin.Interfaces;
using FrostyHavokPlugin.Utils;
using OpenTK.Mathematics;
using Half = System.Half;

namespace FrostyHavokPlugin;

public class PackFileSerializer
{
    private int m_currentLocalWriteQueue;
    private int m_currentSerializationQueue;
    private List<GlobalFixup> m_globalFixups = new();

    private Dictionary<IHavokObject, uint> m_globalLookup = new(ReferenceEqualityComparer.Instance);
    private HKXHeader? m_header;

    private List<LocalFixup> m_localFixups = new();
    private List<Queue<Action>> m_localWriteQueues = new();
    private Dictionary<IHavokObject, List<uint>> m_pendingGlobals = new(ReferenceEqualityComparer.Instance);

    private HashSet<IHavokObject> m_pendingVirtuals = new(ReferenceEqualityComparer.Instance);
    private List<Queue<IHavokObject>> m_serializationQueues = new();

    private HashSet<IHavokObject> m_serializedObjects = new(ReferenceEqualityComparer.Instance);
    private List<VirtualFixup> m_virtualFixups = new();
    private Dictionary<string, uint> m_virtualLookup = new();


    private void PushLocalWriteQueue()
    {
        m_currentLocalWriteQueue++;
        if (m_currentLocalWriteQueue == m_localWriteQueues.Count)
        {
            m_localWriteQueues.Add(new Queue<Action>());
        }
    }

    private void PopLocalWriteQueue()
    {
        m_currentLocalWriteQueue--;
    }

    private void PushSerializationQueue()
    {
        m_currentSerializationQueue++;
        if (m_currentSerializationQueue == m_serializationQueues.Count)
        {
            m_serializationQueues.Add(new Queue<IHavokObject>());
        }
    }

    private void PopSerializationQueue()
    {
        m_currentSerializationQueue--;
    }


    public void Serialize(IHavokObject rootObject, DataStream bw, DataStream fixupTable, HKXHeader header)
    {
        m_header = header;

        m_header.Write(bw);
        // Initialize bookkeeping structures
        m_localFixups = new List<LocalFixup>();
        m_globalFixups = new List<GlobalFixup>();
        m_virtualFixups = new List<VirtualFixup>();

        m_globalLookup = new Dictionary<IHavokObject, uint>(ReferenceEqualityComparer.Instance);
        m_virtualLookup = new Dictionary<string, uint>();

        m_localWriteQueues = new List<Queue<Action>>();
        m_serializationQueues = new List<Queue<IHavokObject>>();
        m_pendingGlobals = new Dictionary<IHavokObject, List<uint>>(ReferenceEqualityComparer.Instance);
        m_pendingVirtuals = new HashSet<IHavokObject>(ReferenceEqualityComparer.Instance);

        m_serializedObjects = new HashSet<IHavokObject>(ReferenceEqualityComparer.Instance);

        // Memory stream for writing all the class definitions
        using BlockStream classbw = new(0);

        // Data memory stream for havok objects
        // debugging
        //var datams = new FileStream(".\\dump.hex", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete, 1, FileOptions.WriteThrough);
        using BlockStream databw = new(0);

        // Populate class names with some stuff havok always has
        HKXClassName hkClass = new() { ClassName = "hkClass", Signature = 0x75585EF6 };
        HKXClassName hkClassMember = new() { ClassName = "hkClassMember", Signature = 0x5C7EA4C2 };
        HKXClassName hkClassEnum = new() { ClassName = "hkClassEnum", Signature = 0x8A3609CF };
        HKXClassName hkClassEnumItem = new() { ClassName = "hkClassEnumItem", Signature = 0xCE6F8A6C };

        hkClass.Write(classbw);
        hkClassMember.Write(classbw);
        hkClassEnum.Write(classbw);
        hkClassEnumItem.Write(classbw);

        m_serializationQueues.Add(new Queue<IHavokObject>());
        m_serializationQueues[0].Enqueue(rootObject);
        m_localWriteQueues.Add(new Queue<Action>());
        m_pendingVirtuals.Add(rootObject);

        while (m_serializationQueues.Count > 1 || m_serializationQueues[0].Count > 0)
        {
            Queue<IHavokObject> sq = m_serializationQueues.Last();

            while (sq.Count == 0 && m_serializationQueues.Count > 1)
            {
                m_serializationQueues.RemoveAt(m_serializationQueues.Count - 1);
                sq = m_serializationQueues.Last();
            }

            if (sq.Count == 0)
            {
                continue;
            }

            IHavokObject obj = sq.Dequeue();
            m_currentSerializationQueue = m_serializationQueues.Count - 1;

            if (m_serializedObjects.Contains(obj))
            {
                continue;
            }

            // See if we need to add virtual bookkeeping
            if (m_pendingVirtuals.Contains(obj))
            {
                m_pendingVirtuals.Remove(obj);
                string classname = obj.GetType().Name;
                if (!m_virtualLookup.ContainsKey(classname))
                {
                    // Need to create a new class name entry and record the position
                    HKXClassName cname = new()
                    {
                        ClassName = classname,
                        Signature = obj.Signature
                    };
                    uint offset = (uint)classbw.Position;
                    cname.Write(classbw);
                    m_virtualLookup.Add(classname, offset + 5);
                }

                // Create a new Virtual fixup for this object
                VirtualFixup vfu = new()
                {
                    Src = (uint)databw.Position,
                    DstSectionIndex = 0,
                    Dst = m_virtualLookup[classname]
                };
                m_virtualFixups.Add(vfu);

                // See if we have any pending global references to this object
                if (m_pendingGlobals.ContainsKey(obj))
                {
                    // If so, create all the needed global fixups
                    foreach (uint src in m_pendingGlobals[obj])
                    {
                        GlobalFixup gfu = new()
                        {
                            Src = src,
                            DstSectionIndex = 2,
                            Dst = (uint)databw.Position
                        };
                        m_globalFixups.Add(gfu);
                    }

                    m_pendingGlobals.Remove(obj);
                }

                // Add global lookup
                m_globalLookup.Add(obj, (uint)databw.Position);
            }

            obj.Write(this, databw);
            m_serializedObjects.Add(obj);
            databw.Pad(16);

            // Write local data (such as array contents and strings)
            while (m_localWriteQueues.Count > 1 || m_localWriteQueues[0].Count > 0)
            {
                Queue<Action> q = m_localWriteQueues.Last();
                while (q.Count == 0 && m_localWriteQueues.Count > 1)
                {
                    if (m_localWriteQueues.Count > 1)
                    {
                        m_localWriteQueues.RemoveAt(m_localWriteQueues.Count - 1);
                    }

                    q = m_localWriteQueues.Last();

                    // Do alignment at the popping of a queue frame
                    databw.Pad(16);
                }

                if (q.Count == 0)
                {
                    m_currentLocalWriteQueue = m_localWriteQueues.Count - 1;
                    continue;
                }

                Action act = q.Dequeue();
                m_currentLocalWriteQueue = m_localWriteQueues.Count - 1;
                act.Invoke();
            }

            databw.Pad(16);
        }

        classbw.Position = 0;
        databw.Position = 0;

        HKXSection classNames = new()
        {
            SectionID = 0,
            SectionTag = "__classnames__",
            SectionData = classbw,
            ContentsVersionString = m_header.ContentsVersionString
        };
        HKXSection types = new() { SectionID = 1, SectionTag = "__types__", SectionData = new BlockStream(), ContentsVersionString = m_header.ContentsVersionString };
        // debugging
        //var ms = new MemoryStream();
        //var tempPosition = datams.Position;
        //datams.Position = 0;
        //datams.CopyTo(ms);
        //datams.Position = tempPosition;

        HKXSection data = new()
        {
            SectionID = 2,
            SectionTag = "__data__",
            SectionData = databw,
            LocalFixups = m_localFixups.OrderBy(x => x.Dst).ToList(),
            GlobalFixups = m_globalFixups.OrderBy(x => x.Src).ToList(),
            VirtualFixups = m_virtualFixups,
            ContentsVersionString = m_header.ContentsVersionString
        };

        classNames.WriteHeader(bw);
        types.WriteHeader(bw);
        data.WriteHeader(bw);

        classNames.WriteData(bw, fixupTable);
        types.WriteData(bw, fixupTable);
        data.WriteData(bw, fixupTable);
    }

    #region Write methods

    public void WriteUSize(DataStream bw, ulong value)
    {
        if (m_header?.PointerSize == 8)
        {
            bw.WriteUInt64(value);
        }
        else
        {
            bw.WriteUInt32((uint)value);
        }
    }

    private void PadToPointerSizeIfPaddingOption(DataStream bw)
    {
        if (m_header?.PaddingOption == 1)
        {
            bw.Pad(m_header.PointerSize);
        }
    }

    public void WriteVoidPointer(DataStream bw)
    {
        PadToPointerSizeIfPaddingOption(bw);
        WriteUSize(bw, 0);
    }

    public void WriteVoidArray(DataStream bw)
    {
        WriteVoidPointer(bw);
        bw.WriteUInt32(0);
        bw.WriteUInt32(0 | ((uint)0x80 << 24));
    }

    private void WriteArrayBase<T>(DataStream bw, IList<T> l, Action<T> perElement, bool pad = false)
    {
        PadToPointerSizeIfPaddingOption(bw);

        uint src = (uint)bw.Position;
        uint size = (uint)l.Count;

        WriteUSize(bw, 0);
        bw.WriteUInt32(size);
        bw.WriteUInt32(size | ((uint)0x80 << 24));

        if (size <= 0)
        {
            return;
        }

        LocalFixup lfu = new() { Src = src };
        m_localFixups.Add(lfu);
        m_localWriteQueues[m_currentLocalWriteQueue].Enqueue(() =>
        {
            bw.Pad(16);
            lfu.Dst = (uint)bw.Position;
            // This ensures any writes the array elements may have are top priority
            PushLocalWriteQueue();
            foreach (T item in l)
            {
                perElement.Invoke(item);
            }

            PopLocalWriteQueue();
        });
        if (pad)
        {
            m_localWriteQueues[m_currentLocalWriteQueue].Enqueue(() => { bw.Pad(16); });
        }
    }

    private void WriteRelArrayBase<T>(DataStream bw, IList<T> l, Action<T> perElement, bool pad = false)
    {
        uint src = (uint)bw.Position;
        ushort size = (ushort)l.Count;

        bw.WriteUInt16(size);
        bw.WriteUInt16(0);

        if (size <= 0)
        {
            return;
        }

        m_localWriteQueues[m_currentLocalWriteQueue].Enqueue(() =>
        {
            bw.Pad(16);
            long dst = bw.Position;
            bw.Position = src + 2;
            bw.WriteUInt16((ushort)(dst - src));
            bw.Position = dst;
            // This ensures any writes the array elements may have are top priority
            PushLocalWriteQueue();
            foreach (T item in l)
            {
                perElement.Invoke(item);
            }

            PopLocalWriteQueue();
        });
        if (pad)
        {
            m_localWriteQueues[m_currentLocalWriteQueue].Enqueue(() => { bw.Pad(16); });
        }
    }

    public void WriteClassArray<T>(DataStream bw, IList<T> d) where T : IHavokObject
    {
        WriteArrayBase(bw, d, e => { e.Write(this, bw); }, true);
    }

    public void WriteClassRelArray<T>(DataStream bw, IList<T> d) where T : IHavokObject
    {
        WriteRelArrayBase(bw, d, e => { e.Write(this, bw); }, true);
    }

    public void WriteClassPointer<T>(DataStream bw, T? d) where T : IHavokObject
    {
        PadToPointerSizeIfPaddingOption(bw);
        uint pos = (uint)bw.Position;
        WriteUSize(bw, 0);

        if (d == null)
        {
            return;
        }

        // If we're referencing an already serialized object, add a global ref
        if (m_globalLookup.ContainsKey(d))
        {
            GlobalFixup gfu = new() { Src = pos, DstSectionIndex = 2, Dst = m_globalLookup[d] };
            m_globalFixups.Add(gfu);
            return;
        }
        // Otherwise need to add a pending reference and mark the object for serialization

        if (!m_pendingGlobals.ContainsKey(d))
        {
            m_pendingGlobals.Add(d, new List<uint>());
            PushSerializationQueue();
            m_serializationQueues[m_currentSerializationQueue].Enqueue(d);
            PopSerializationQueue();
            m_pendingVirtuals.Add(d);
        }

        m_pendingGlobals[d].Add(pos);
    }

    public void WriteClassPointerArray<T>(DataStream bw, IList<T> d) where T : IHavokObject
    {
        WriteArrayBase(bw, d, e => WriteClassPointer(bw, e));
    }

    public void WriteClassPointerRelArray<T>(DataStream bw, IList<T> d) where T : IHavokObject
    {
        WriteRelArrayBase(bw, d, e => WriteClassPointer(bw, e));
    }

    public void WriteStringPointer(DataStream bw, string? d, int padding = 16)
    {
        PadToPointerSizeIfPaddingOption(bw);
        uint src = (uint)bw.Position;
        WriteUSize(bw, 0);

        if (string.IsNullOrEmpty(d))
        {
            return;
        }

        LocalFixup lfu = new() { Src = src };
        m_localFixups.Add(lfu);
        m_localWriteQueues[m_currentLocalWriteQueue].Enqueue(() =>
        {
            lfu.Dst = (uint)bw.Position;
            bw.WriteNullTerminatedString(d);
            bw.Pad(padding);
        });
    }

    public void WriteCString(DataStream bw, string? d, int padding = 16)
    {
        // difference with StirngPointer
        // when empty string it didn't create localFixup
        PadToPointerSizeIfPaddingOption(bw);
        uint src = (uint)bw.Position;
        WriteUSize(bw, 0);

        if (string.IsNullOrEmpty(d))
        {
            return;
        }

        LocalFixup lfu = new() { Src = src };
        m_localFixups.Add(lfu);
        m_localWriteQueues[m_currentLocalWriteQueue].Enqueue(() =>
        {
            lfu.Dst = (uint)bw.Position;
            bw.WriteNullTerminatedString(d);
            bw.Pad(padding);
        });
    }

    public void WriteStringPointerArray(DataStream bw, IList<string> d)
    {
        WriteArrayBase(bw, d, e => { WriteStringPointer(bw, e, 2); });
    }

    public void WriteStringPointerRelArray(DataStream bw, IList<string> d)
    {
        WriteRelArrayBase(bw, d, e => { WriteStringPointer(bw, e, 2); });
    }

    public void WriteByte(DataStream bw, byte d)
    {
        bw.WriteByte(d);
    }

    public void WriteByteArray(DataStream bw, IList<byte> d)
    {
        WriteArrayBase(bw, d, e => WriteByte(bw, e));
    }

    public void WriteByteRelArray(DataStream bw, IList<byte> d)
    {
        WriteRelArrayBase(bw, d, e => WriteByte(bw, e));
    }

    public void WriteSByte(DataStream bw, sbyte d)
    {
        bw.WriteSByte(d);
    }

    public void WriteSByteArray(DataStream bw, IList<sbyte> d)
    {
        WriteArrayBase(bw, d, e => WriteSByte(bw, e));
    }

    public void WriteSByteRelArray(DataStream bw, IList<sbyte> d)
    {
        WriteRelArrayBase(bw, d, e => WriteSByte(bw, e));
    }


    public void WriteUInt16(DataStream bw, ushort d)
    {
        bw.WriteUInt16(d);
    }

    public void WriteUInt16Array(DataStream bw, IList<ushort> d)
    {
        WriteArrayBase(bw, d, e => WriteUInt16(bw, e));
    }

    public void WriteUInt16RelArray(DataStream bw, IList<ushort> d)
    {
        WriteRelArrayBase(bw, d, e => WriteUInt16(bw, e));
    }

    public void WriteInt16(DataStream bw, short d)
    {
        bw.WriteInt16(d);
    }

    public void WriteInt16Array(DataStream bw, IList<short> d)
    {
        WriteArrayBase(bw, d, e => WriteInt16(bw, e));
    }

    public void WriteInt16RelArray(DataStream bw, IList<short> d)
    {
        WriteRelArrayBase(bw, d, e => WriteInt16(bw, e));
    }

    public void WriteUInt32(DataStream bw, uint d)
    {
        bw.WriteUInt32(d);
    }

    public void WriteUInt32Array(DataStream bw, IList<uint> d)
    {
        WriteArrayBase(bw, d, e => WriteUInt32(bw, e));
    }

    public void WriteUInt32RelArray(DataStream bw, IList<uint> d)
    {
        WriteRelArrayBase(bw, d, e => WriteUInt32(bw, e));
    }

    public void WriteInt32(DataStream bw, int d)
    {
        bw.WriteInt32(d);
    }

    public void WriteInt32Array(DataStream bw, IList<int> d)
    {
        WriteArrayBase(bw, d, e => WriteInt32(bw, e));
    }

    public void WriteInt32RelArray(DataStream bw, IList<int> d)
    {
        WriteRelArrayBase(bw, d, e => WriteInt32(bw, e));
    }

    public void WriteUInt64(DataStream bw, ulong d)
    {
        bw.WriteUInt64(d);
    }

    public void WriteUInt64Array(DataStream bw, IList<ulong> d)
    {
        WriteArrayBase(bw, d, e => WriteUInt64(bw, e));
    }

    public void WriteUInt64RelArray(DataStream bw, IList<ulong> d)
    {
        WriteRelArrayBase(bw, d, e => WriteUInt64(bw, e));
    }

    public void WriteInt64(DataStream bw, long d)
    {
        bw.WriteInt64(d);
    }

    public void WriteInt64Array(DataStream bw, IList<long> d)
    {
        WriteArrayBase(bw, d, e => WriteInt64(bw, e));
    }

    public void WriteInt64RelArray(DataStream bw, IList<long> d)
    {
        WriteRelArrayBase(bw, d, e => WriteInt64(bw, e));
    }

    public void WriteHalf(DataStream bw, Half d)
    {
        bw.WriteHalf(d);
    }

    public void WriteHalfArray(DataStream bw, IList<Half> d)
    {
        WriteArrayBase(bw, d, e => WriteHalf(bw, e));
    }

    public void WriteHalfRelArray(DataStream bw, IList<Half> d)
    {
        WriteRelArrayBase(bw, d, e => WriteHalf(bw, e));
    }

    public void WriteSingle(DataStream bw, float d)
    {
        bw.WriteSingle(d);
    }

    public void WriteSingleArray(DataStream bw, IList<float> d)
    {
        WriteArrayBase(bw, d, e => WriteSingle(bw, e));
    }

    public void WriteSingleRelArray(DataStream bw, IList<float> d)
    {
        WriteRelArrayBase(bw, d, e => WriteSingle(bw, e));
    }

    public void WriteBoolean(DataStream bw, bool d)
    {
        bw.WriteBoolean(d);
    }

    public void WriteBooleanArray(DataStream bw, IList<bool> d)
    {
        WriteArrayBase(bw, d, e => WriteBoolean(bw, e));
    }

    public void WriteBooleanRelArray(DataStream bw, IList<bool> d)
    {
        WriteRelArrayBase(bw, d, e => WriteBoolean(bw, e));
    }

    public void WriteVector4(DataStream bw, Vector4 d)
    {
        bw.WriteVector4(d);
    }

    public void WriteVector4Array(DataStream bw, IList<Vector4> d)
    {
        WriteArrayBase(bw, d, e => WriteVector4(bw, e));
    }

    public void WriteVector4RelArray(DataStream bw, IList<Vector4> d)
    {
        WriteRelArrayBase(bw, d, e => WriteVector4(bw, e));
    }

    public void WriteMatrix3(DataStream bw, Matrix3x4 d)
    {
        bw.WriteSingle(d.M11);
        bw.WriteSingle(d.M12);
        bw.WriteSingle(d.M13);
        bw.WriteSingle(d.M14);
        bw.WriteSingle(d.M21);
        bw.WriteSingle(d.M22);
        bw.WriteSingle(d.M23);
        bw.WriteSingle(d.M24);
        bw.WriteSingle(d.M31);
        bw.WriteSingle(d.M32);
        bw.WriteSingle(d.M33);
        bw.WriteSingle(d.M34);
    }

    public void WriteMatrix3Array(DataStream bw, IList<Matrix3x4> d)
    {
        WriteArrayBase(bw, d, e => WriteMatrix3(bw, e));
    }

    public void WriteMatrix3RelArray(DataStream bw, IList<Matrix3x4> d)
    {
        WriteRelArrayBase(bw, d, e => WriteMatrix3(bw, e));
    }

    public void WriteMatrix4(DataStream bw, Matrix4 d)
    {
        bw.WriteSingle(d.M11);
        bw.WriteSingle(d.M12);
        bw.WriteSingle(d.M13);
        bw.WriteSingle(d.M14);
        bw.WriteSingle(d.M21);
        bw.WriteSingle(d.M22);
        bw.WriteSingle(d.M23);
        bw.WriteSingle(d.M24);
        bw.WriteSingle(d.M31);
        bw.WriteSingle(d.M32);
        bw.WriteSingle(d.M33);
        bw.WriteSingle(d.M34);
        bw.WriteSingle(d.M41);
        bw.WriteSingle(d.M42);
        bw.WriteSingle(d.M43);
        bw.WriteSingle(d.M44);
    }

    public void WriteMatrix4Array(DataStream bw, IList<Matrix4> d)
    {
        WriteArrayBase(bw, d, e => WriteMatrix4(bw, e));
    }

    public void WriteMatrix4RelArray(DataStream bw, IList<Matrix4> d)
    {
        WriteRelArrayBase(bw, d, e => WriteMatrix4(bw, e));
    }

    public void WriteTransform(DataStream bw, Matrix4 d)
    {
        bw.WriteSingle(d.M11);
        bw.WriteSingle(d.M12);
        bw.WriteSingle(d.M13);
        bw.WriteSingle(d.M14);
        bw.WriteSingle(d.M21);
        bw.WriteSingle(d.M22);
        bw.WriteSingle(d.M23);
        bw.WriteSingle(d.M24);
        bw.WriteSingle(d.M31);
        bw.WriteSingle(d.M32);
        bw.WriteSingle(d.M33);
        bw.WriteSingle(d.M34);
        bw.WriteSingle(d.M41);
        bw.WriteSingle(d.M42);
        bw.WriteSingle(d.M43);
        bw.WriteSingle(d.M44);
    }

    public void WriteTransformArray(DataStream bw, IList<Matrix4> d)
    {
        WriteArrayBase(bw, d, e => WriteTransform(bw, e));
    }

    public void WriteTransformRelArray(DataStream bw, IList<Matrix4> d)
    {
        WriteRelArrayBase(bw, d, e => WriteTransform(bw, e));
    }

    public void WriteQSTransform(DataStream bw, Matrix3x4 d)
    {
        WriteMatrix3(bw, d);
    }

    public void WriteQSTransformArray(DataStream bw, IList<Matrix3x4> d)
    {
        WriteMatrix3Array(bw, d);
    }

    public void WriteQSTransformRelArray(DataStream bw, IList<Matrix3x4> d)
    {
        WriteMatrix3RelArray(bw, d);
    }

    public void WriteQuaternion(DataStream bw, Quaternion d)
    {
        bw.WriteSingle(d.X);
        bw.WriteSingle(d.Y);
        bw.WriteSingle(d.Z);
        bw.WriteSingle(d.W);
    }

    public void WriteQuaternionArray(DataStream bw, IList<Quaternion> d)
    {
        WriteArrayBase(bw, d, e => WriteQuaternion(bw, e));
    }

    #region C Style Array

    private void WriteCStyleArrayBase<T>(IList<T> content, Action<T> perElement)
    {
        foreach (T item in content)
        {
            perElement.Invoke(item);
        }
    }

    public void WriteBooleanCStyleArray(DataStream bw, bool[] d)
    {
        WriteCStyleArrayBase(d, e => WriteBoolean(bw, e));
    }

    public void WriteByteCStyleArray(DataStream bw, byte[] d)
    {
        WriteCStyleArrayBase(d, e => WriteByte(bw, e));
    }

    public void WriteSByteCStyleArray(DataStream bw, sbyte[] d)
    {
        WriteCStyleArrayBase(d, e => WriteSByte(bw, e));
    }

    public void WriteInt16CStyleArray(DataStream bw, short[] d)
    {
        WriteCStyleArrayBase(d, e => WriteInt16(bw, e));
    }

    public void WriteUInt16CStyleArray(DataStream bw, ushort[] d)
    {
        WriteCStyleArrayBase(d, e => WriteUInt16(bw, e));
    }

    public void WriteInt32CStyleArray(DataStream bw, int[] d)
    {
        WriteCStyleArrayBase(d, e => WriteInt32(bw, e));
    }

    public void WriteUInt32CStyleArray(DataStream bw, uint[] d)
    {
        WriteCStyleArrayBase(d, e => WriteUInt32(bw, e));
    }

    public void WriteInt64CStyleArray(DataStream bw, long[] d)
    {
        WriteCStyleArrayBase(d, e => WriteInt64(bw, e));
    }

    public void WriteUInt64CStyleArray(DataStream bw, ulong[] d)
    {
        WriteCStyleArrayBase(d, e => WriteUInt64(bw, e));
    }

    public void WriteHalfCStyleArray(DataStream bw, Half[] d)
    {
        WriteCStyleArrayBase(d, e => WriteHalf(bw, e));
    }

    public void WriteSingleCStyleArray(DataStream bw, float[] d)
    {
        WriteCStyleArrayBase(d, e => WriteSingle(bw, e));
    }

    public void WriteVector4CStyleArray(DataStream bw, Vector4[] d)
    {
        WriteCStyleArrayBase(d, e => WriteVector4(bw, e));
    }

    public void WriteQuaternionCStyleArray(DataStream bw, Quaternion[] d)
    {
        WriteCStyleArrayBase(d, e => WriteQuaternion(bw, e));
    }

    public void WriteMatrix3CStyleArray(DataStream bw, Matrix3x4[] d)
    {
        WriteCStyleArrayBase(d, e => WriteMatrix3(bw, e));
    }

    public void WriteRotationCStyleArray(DataStream bw, Matrix3x4[] d)
    {
        WriteCStyleArrayBase(d, e => WriteMatrix3(bw, e));
    }

    public void WriteQSTransformCStyleArray(DataStream bw, Matrix3x4[] d)
    {
        WriteCStyleArrayBase(d, e => WriteQSTransform(bw, e));
    }

    public void WriteMatrix4CStyleArray(DataStream bw, Matrix4[] d)
    {
        WriteCStyleArrayBase(d, e => WriteMatrix4(bw, e));
    }

    public void WriteTransformCStyleArray(DataStream bw, Matrix4[] d)
    {
        WriteCStyleArrayBase(d, e => WriteTransform(bw, e));
    }

    public void WriteClassPointerCStyleArray<T>(DataStream bw, T?[] d) where T : IHavokObject, new()
    {
        WriteCStyleArrayBase(d, e => WriteClassPointer(bw, e));
    }

    public void WriteEmptyPointerCStyleArray(DataStream bw, short length)
    {
        for (int i = 0; i < length; i++)
        {
            WriteVoidPointer(bw);
        }
    }

    public void WriteStructCStyleArray<T>(DataStream bw, T[] d) where T : IHavokObject
    {
        foreach (T item in d)
        {
            item.Write(this, bw);
        }
    }

    #endregion

    #endregion
}