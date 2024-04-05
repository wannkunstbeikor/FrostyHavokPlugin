using System.Buffers.Binary;
using System.Text;
using Frosty.Sdk.IO;
using Frosty.Sdk.Resources;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using hk;
using OpenTK.Mathematics;

namespace FrostyHavokPlugin;

public class HavokPhysicsData : Resource
{
    public int PartCount { get; private set; }

    public override void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        uint packFilesOffset = BinaryPrimitives.ReadUInt16LittleEndian(inResMeta.Slice(0, 2));
        uint firstPackFileSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(4, 4));
        uint secondPackFileSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(8, 4));
        uint fixupTablesSize = BinaryPrimitives.ReadUInt32LittleEndian(inResMeta.Slice(12, 4));

        PartCount = inStream.ReadInt32();

        int partTranslationsCount = inStream.ReadInt32();
        long partTranslationsOffset = inStream.ReadInt64(); // vec3
        int localAabbsCount = inStream.ReadInt32();
        long localAabbsOffset = inStream.ReadInt64(); // aabb
        int materialIndicesCount = inStream.ReadInt32();
        long materialIndicesOffset = inStream.ReadInt64(); // byte
        int materialFlagsAndIndicesCount = inStream.ReadInt32();
        long materialFlagsAndIndicesOffset = inStream.ReadInt64(); // uint

        int unkCount = inStream.ReadInt32();
        long unkOffset = inStream.ReadInt64(); // ushort

        inStream.Position = packFilesOffset + firstPackFileSize + secondPackFileSize;
        int firstFixupTableSize = inStream.ReadInt32();
        int secondFixupTableSize = inStream.ReadInt32();
        uint fixupTablesOffset = (uint)inStream.Position;

        inStream.Position = packFilesOffset;

        PackFileDeserializer deserializer = new();

        //deserializer.Deserialize(inStream, fixupTablesOffset);

        inStream.Position = packFilesOffset + firstPackFileSize;

        IHavokObject obj = deserializer.Deserialize(inStream, (uint)(fixupTablesOffset + firstFixupTableSize));

        var root = obj as hkRootLevelContainer;

        var container = root!._namedVariants[0]._variant as HavokPhysicsContainer;

        StringBuilder sb = new();
        int current = 0;
        int currentVertex = 0;
        foreach (hknpShape shape in container!._shapes)
        {
            if (shape is hknpStaticCompoundShape compound)
            {
                foreach (var instance in compound._instances._elements)
                {
                    if (instance._shape is hknpConvexPolytopeShape poly)
                    {
                        sb.AppendLine($"o shape{current++}");
                        foreach (Vector4 vertex in poly._vertices)
                        {
                            sb.AppendLine($"v {vertex.X} {vertex.Y} {vertex.Z} {vertex.W}");
                        }

                        foreach (hknpConvexPolytopeShapeFace face in poly._faces)
                        {
                            sb.Append("f");

                            for (int i = 0; i < face._numIndices; i++)
                            {
                                sb.Append($" {poly._indices[face._firstIndex + i] + currentVertex + 1}");
                            }

                            sb.AppendLine();
                        }

                        currentVertex += poly._vertices.Count;
                    }
                }
            }
        }

        File.WriteAllText("/home/jona/havok.obj", sb.ToString());
    }

    public override void Serialize(DataStream stream, Span<byte> resMeta)
    {
        throw new NotImplementedException();
    }
}