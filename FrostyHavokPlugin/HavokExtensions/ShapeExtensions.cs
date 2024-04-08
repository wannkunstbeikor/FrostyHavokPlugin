using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;
using FrostyHavokPlugin.Utils;
using hk;
using OpenTK.Mathematics;

namespace FrostyHavokPlugin.HavokExtensions;

public static class ShapeExtensions
{
    public static void Export(this hknpShape shape, ObjWriter builder, string name)
    {
        switch (shape)
        {
            case hknpStaticCompoundShape compoundShape:
            {
                int i = 0;
                foreach (hknpShapeInstance instance in compoundShape._instances._elements)
                {
                    instance._shape.Export(builder, $"staticcompound-{name}-shape-{i++}");
                }

                compoundShape.ExportAabbs(builder, name);

                break;
            }
            case hknpConvexPolytopeShape convex:
            {
                builder.WriteObject($"convexpolytope-{name}");
                foreach (Vector4 vertex in convex._vertices)
                {
                    builder.WriteVertex(vertex);

                    int index = BitConverter.ToInt32(BitConverter.GetBytes(vertex.W)) & ~0x3f000000;
                }

                foreach (Vector4 plane in convex._planes)
                {
                    Vector3 vec = new(plane);
                    float w = Vector3.Dot(vec, Vector3.UnitX);
                }

                foreach (hknpConvexPolytopeShapeFace face in convex._faces)
                {
                    Span<byte> indices = new(convex._indices.ToArray());
                    builder.WriteFace(indices.Slice(face._firstIndex, face._numIndices).ToArray());



                }

                break;
            }
        }
    }

    public static void ExportAabbs(this hknpStaticCompoundShape shape, ObjWriter builder, string name)
    {
        Box3 aabb = new (new Vector3(shape._boundingVolumeData._aabbTree._domain._min), new Vector3(shape._boundingVolumeData._aabbTree._domain._max));
        if (shape._boundingVolumeData._aabbTree._nodes.Count == 0)
        {
            return;
        }

        int index = 0;
        ParseNode(shape._boundingVolumeData._aabbTree._nodes[index++], aabb, builder, () => shape._boundingVolumeData._aabbTree._nodes[index++], name);
    }

    private static void ParseNode(hkcdStaticTreeCodec3Axis6 node, Box3 aabb, ObjWriter builder, Func<hkcdStaticTreeCodec3Axis6> getNext, string name)
    {
        Vector3 scale = aabb.Max - aabb.Min;
        Vector3 constant = new(1f / 226, 1f / 226, 1f / 226);
        scale *= constant;

        Vector3 lower = new(node._xyz[0] & 0xF, node._xyz[1] & 0xF, node._xyz[2] & 0xF);
        Vector3 higher = new(node._xyz[0] >> 4, node._xyz[1] >> 4, node._xyz[2] >> 4);

        Vector3 minValue = higher * higher;
        Vector3 maxValue = lower * lower;

        Box3 newAabb = aabb;
        newAabb.Min += scale * minValue;
        newAabb.Max -= scale * maxValue;

        if ((node._hiData & 0x80) != 0)
        {
            // internal
            int delta = (((node._hiData & ~0x80) << 0x10) << 1) | (node._loData << 1);
            // number of children leaf nodes probably
            ParseNode(getNext(), newAabb, builder, getNext, name);
            ParseNode(getNext(), newAabb, builder, getNext, name);
        }
        else
        {
            // leaf
            int data = node._loData | (node._hiData << 0x10);
            // index of shape probably
            newAabb.CreateAabb($"{name}-aabb-{data}", builder);
        }
    }

    public static void CreateAabb(this Box3 aabb, string name, ObjWriter builder)
    {
        builder.WriteObject(name);
        builder.WriteVertex(aabb.Min.X, aabb.Min.Y, aabb.Min.Z);
        builder.WriteVertex(aabb.Min.X, aabb.Max.Y, aabb.Min.Z);
        builder.WriteVertex(aabb.Max.X, aabb.Max.Y, aabb.Min.Z);
        builder.WriteVertex(aabb.Max.X, aabb.Min.Y, aabb.Min.Z);

        builder.WriteVertex(aabb.Min.X, aabb.Min.Y, aabb.Max.Z);
        builder.WriteVertex(aabb.Min.X, aabb.Max.Y, aabb.Max.Z);
        builder.WriteVertex(aabb.Max.X, aabb.Max.Y, aabb.Max.Z);
        builder.WriteVertex(aabb.Max.X, aabb.Min.Y, aabb.Max.Z);

        builder.WriteLine([0, 1, 2, 3, 0]);
        builder.WriteLine([4, 5, 6, 7, 4]);
        builder.WriteLine([0, 1, 5, 4, 0]);
        builder.WriteLine([2, 3, 7, 6, 2]);
    }
}