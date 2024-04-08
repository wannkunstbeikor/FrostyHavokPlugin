using System.Buffers.Binary;
using Frosty.Sdk;
using Frosty.Sdk.IO;
using Frosty.Sdk.Resources;
using Frosty.Sdk.Utils;
using FrostyHavokPlugin.CommonTypes;
using FrostyHavokPlugin.HavokExtensions;
using FrostyHavokPlugin.Interfaces;
using FrostyHavokPlugin.Utils;
using hk;
using OpenTK.Mathematics;

namespace FrostyHavokPlugin;

public class HavokPhysicsData : Resource
{
    public int PartCount { get; private set; }
    public List<Vector3> PartTranslations { get; private set; } = new();
    public List<Box3> LocalAabbs { get; private set; } = new();
    public List<byte> MaterialIndices { get; private set; } = new();
    public List<uint> MaterialFlagsAndIndices { get; private set; } = new();
    public List<ushort> DetailResourceIndices { get; private set; } = new();
    public byte MaterialCountUsed { get; private set; }
    public byte HighestMaterialIndex { get; private set; }

    private Block<byte>? m_firstPackFile;
    private HKXHeader? m_header;
    private IHavokObject? m_obj;

    public override void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        uint packFilesOffset = BinaryPrimitives.ReadUInt16LittleEndian(inResMeta.Slice(0, 2));
        uint firstPackFileSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(4, 4));
        uint secondPackFileSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(8, 4));
        uint fixupTablesSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(12, 4));

        PartCount = inStream.ReadInt32();

        int count = inStream.ReadInt32();
        PartTranslations.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                PartTranslations.Add(stream.ReadVector3());
            }
        });


        count = inStream.ReadInt32();
        LocalAabbs.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                LocalAabbs.Add(stream.ReadAabb());
            }
        });

        count = inStream.ReadInt32();
        MaterialIndices.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                MaterialIndices.Add(stream.ReadByte());
            }
        });

        count = inStream.ReadInt32();
        MaterialFlagsAndIndices.EnsureCapacity(count);
        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < count; i++)
            {
                MaterialFlagsAndIndices.Add(stream.ReadUInt32());
            }
        });

        // TODO: check when they added this
        if (ProfilesLibrary.FrostbiteVersion > "2016")
        {
            count = inStream.ReadInt32();
            DetailResourceIndices.EnsureCapacity(count);
            inStream.ReadRelocPtr(stream =>
            {
                for (int i = 0; i < count; i++)
                {
                    DetailResourceIndices.Add(stream.ReadUInt16());
                }
            });
        }

        MaterialCountUsed = inStream.ReadByte();
        HighestMaterialIndex = inStream.ReadByte();

        inStream.Position = packFilesOffset + firstPackFileSize + secondPackFileSize;
        int firstFixupTableSize = inStream.ReadInt32();
        int secondFixupTableSize = inStream.ReadInt32();
        uint fixupTablesOffset = (uint)inStream.Position;

        // reloc table after fixup table

        inStream.Position = packFilesOffset;
        m_firstPackFile = new Block<byte>((int)firstPackFileSize);
        inStream.ReadExactly(m_firstPackFile);

        PackFileDeserializer deserializer = new();

        m_obj = deserializer.Deserialize(inStream, (uint)(fixupTablesOffset + firstFixupTableSize));
        m_header = deserializer.Header;

         hkRootLevelContainer? root = m_obj as hkRootLevelContainer;

         HavokPhysicsContainer? container = root!._namedVariants[0]._variant as HavokPhysicsContainer;

         int current = 0;

         ObjWriter writer = new();
         foreach (hknpShape shape in container!._shapes)
         {
             shape.Export(writer, $"{current++}");
         }

        writer.WriteToFile("/home/jona/havok.obj");

    }

    public override void Serialize(DataStream inStream, Span<byte> inResMeta)
    {
        // write frostbite stuff
        inStream.WriteInt32(PartCount);

        inStream.WriteInt32(PartTranslations.Count);
        inStream.WriteRelocPtr(nameof(PartTranslations));
        inStream.WriteInt32(LocalAabbs.Count);
        inStream.WriteRelocPtr(nameof(LocalAabbs));
        inStream.WriteInt32(MaterialIndices.Count);
        inStream.WriteRelocPtr(nameof(MaterialIndices));
        inStream.WriteInt32(MaterialFlagsAndIndices.Count);
        inStream.WriteRelocPtr(nameof(MaterialFlagsAndIndices));

        if (ProfilesLibrary.FrostbiteVersion > "2016")
        {
            inStream.WriteInt32(DetailResourceIndices.Count);
            inStream.WriteRelocPtr(nameof(DetailResourceIndices));
        }

        inStream.WriteByte(MaterialCountUsed);
        inStream.WriteByte(HighestMaterialIndex);

        inStream.Pad(16);

        inStream.AddRelocData(nameof(PartTranslations));
        foreach (Vector3 translation in PartTranslations)
        {
            inStream.WriteVector3(translation);
        }

        inStream.AddRelocData(nameof(LocalAabbs));
        foreach (Box3 aabb in LocalAabbs)
        {
            inStream.WriteAabb(aabb);
        }

        inStream.AddRelocData(nameof(MaterialIndices));
        foreach (byte index in MaterialIndices)
        {
            inStream.WriteByte(index);
        }
        inStream.Pad(16);

        inStream.AddRelocData(nameof(MaterialFlagsAndIndices));
        foreach (uint value in MaterialFlagsAndIndices)
        {
            inStream.WriteUInt32(value);
        }
        inStream.Pad(16);

        if (ProfilesLibrary.FrostbiteVersion > "2016")
        {
            inStream.AddRelocData(nameof(DetailResourceIndices));
            foreach (ushort index in DetailResourceIndices)
            {
                inStream.WriteUInt16(index);
            }
            inStream.Pad(16);
        }

        // write packfilesOffset to meta
        BinaryPrimitives.WriteUInt16LittleEndian(inResMeta.Slice(0, 2), (ushort)inStream.Position);

        // we are just copying the 32 bit pack file
        inStream.Write(m_firstPackFile!);
        BinaryPrimitives.WriteInt32LittleEndian(inResMeta.Slice(4, 4), m_firstPackFile!.Size);

        PackFileSerializer serializer = new();
        using Block<byte> data = new(0);
        using Block<byte> fixupTable = new(0);
        using (BlockStream fixupTableStream = new(fixupTable, true))
        using (BlockStream stream = new(data, true))
        {
            serializer.Serialize(m_obj!, stream, fixupTableStream, m_header!);
        }

        inStream.Write(data);
        BinaryPrimitives.WriteInt32LittleEndian(inResMeta.Slice(8, 4), data.Size);

        inStream.WriteInt32(0);
        inStream.WriteInt32(fixupTable.Size);
        inStream.Write(fixupTable);
        BinaryPrimitives.WriteInt32LittleEndian(inResMeta.Slice(12, 4), fixupTable.Size + 8);

        inStream.WriteRelocTable();
    }
}