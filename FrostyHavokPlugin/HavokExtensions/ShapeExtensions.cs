using System;
using System.Buffers.Binary;
using System.Diagnostics;
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
                compoundShape.ComputeAabbTree();

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

                int k = 0;
                foreach (hknpConvexPolytopeShapeFace face in convex._faces)
                {
                    Span<byte> indices = new(convex._indices.ToArray());
                    builder.WriteFace(indices.Slice(face._firstIndex, face._numIndices).ToArray());

                    Vector4 planePlusW = convex._planes[k++];
                    Vector3 plane = new(planePlusW);
                    float distance = 0.0f;
                    for (int i = 0; i < face._numIndices; i++)
                    {
                        Vector3 vertex = new(convex._vertices[convex._indices[face._firstIndex + i]]);

                        distance += -Vector3.Dot(vertex, plane);

                    }
                    distance /= face._numIndices;
                    Debug.Assert(Math.Abs(distance - planePlusW.W) < 0.001);
                }

                break;
            }
        }
    }

    public static Box3 GetAabb(this hknpShape shape)
    {
        switch (shape)
        {
            case hknpCompoundShape compoundShape:
                if (compoundShape._aabb is null)
                {
                    return new Box3();
                }
                var box = new Box3(new Vector3(compoundShape._aabb._min), new Vector3(compoundShape._aabb._max));

                return box;
            case hknpConvexShape convex:
            {
                Box3 retVal = new();
                foreach (Vector4 vertex in convex._vertices)
                {
                    retVal.Extend(new Vector3(vertex));
                }

                return retVal;
            }
            default:
                throw new NotImplementedException();
        }
    }

    public static Box3 unpack(Box3 aabb, hkcdStaticTreeCodec3Axis6 codec3)
    {
        Vector3 scale = aabb.Max - aabb.Min;
        Vector3 constant = new(1f / 226, 1f / 226, 1f / 226);
        scale *= constant;

        Vector3 lower = new(codec3._xyz[0] & 0xF, codec3._xyz[1] & 0xF, codec3._xyz[2] & 0xF);
        Vector3 higher = new(codec3._xyz[0] >> 4, codec3._xyz[1] >> 4, codec3._xyz[2] >> 4);

        Vector3 minValue = higher * higher;
        Vector3 maxValue = lower * lower;

        Box3 newAabb = aabb;
        newAabb.Min += scale * minValue;
        newAabb.Max -= scale * maxValue;
        return newAabb;
    }

    public static void ComputeAabbTree(this hknpStaticCompoundShape shape)
    {
        int count = shape._instances._elements.Count * 2 - 1;

        List<hkcdStaticTreeCodec3Axis6> nodes = new(count);
        List<Box3> aabbs = new(shape._instances._elements.Count);

        Box3 domain = new();
        foreach (hknpShapeInstance instance in shape._instances._elements)
        {
            // TODO: get aabb from shape
            Box3 aabb = instance._shape.GetAabb();
            aabbs.Add(aabb);

            domain.Extend(aabb.Min);
            domain.Extend(aabb.Max);
        }

        shape._boundingVolumeData._aabbTree._domain = new hkAabb { _max = new(domain.Max), _min = new(domain.Min) };
        shape._boundingVolumeData._aabbTree._nodes.Clear();

        // TODO: group aabbs which are similar so we get a good quality tree

        int index = 0;
        int numChildrenLeafNodes = 0;

        Box3 parentAabb = domain, originalAabb = aabbs[index];


        for (int i = 0; i < count; i++)
        {
            bool isLeaf = true;
            int data = 0; // TODO:
            hkcdStaticTreeCodec3Axis6 codec = new();
            parentAabb = Pack(codec, data, isLeaf, parentAabb, originalAabb);
            shape._boundingVolumeData._aabbTree._nodes.Add(codec);
        }
    }

    private static Box3 Pack(hkcdStaticTreeCodec3Axis6 codec, int data, bool isLeaf, Box3 parentAabb,
        Box3 originalAabb)
    {
        if (isLeaf)
        {
            // set data to index of instance, if its a leaf node
            codec._loData = (ushort)data;
            codec._hiData = (byte)(data >> 0x10);
        }
        else
        {
            // set data to number of children leaf nodes, if its not a leaf node
            codec._loData = (ushort)(data >> 1);
            codec._hiData = (byte)((data >> 1 >> 0x10) | 0x80);
        }

        // generate xyz values
        for (int i = 0; i <= 2; i++)
        {
            GenerateHigherBits(codec, i, parentAabb, originalAabb);
            GenerateLowerBits(codec, i, parentAabb, originalAabb);
        }

        // just check if the generated aabb contains the original aabb and is contained by the parent
        Box3 finalAabb = unpack(parentAabb, codec);

        if (!parentAabb.Contains(finalAabb))
        {
            throw new Exception();
        }
        if (!finalAabb.Contains(originalAabb))
        {
            throw new Exception();
        }

        return finalAabb;
    }

    private static void GenerateLowerBits(hkcdStaticTreeCodec3Axis6 codec, int i, Box3 parentAabb, Box3 originalAabb)
    {
        byte value;
        float aabbValue, originalAabbValue;
        do {
            if (0xe < (codec._xyz[i] & 0xf))
            {
                return;
            }

            value = codec._xyz[i];
            codec._xyz[i] += 1;
            Box3 newAabb = unpack(parentAabb,codec);
            aabbValue = newAabb.Max[i];
            originalAabbValue = originalAabb.Max[i];
        } while (originalAabbValue <= aabbValue);

        codec._xyz[i] = value;
    }

    private static void GenerateHigherBits(hkcdStaticTreeCodec3Axis6 codec, int i, Box3 parentAabb, Box3 originalAabb)
    {
        byte value;
        float aabbValue, originalAabbValue;
        do {
            if (0xe < (uint)(codec._xyz[i] >> 4))
            {
                return;
            }

            value = codec._xyz[i];
            codec._xyz[i] += 0x10;
            Box3 newAabb = unpack(parentAabb,codec);
            aabbValue = newAabb.Min[i];
            originalAabbValue = originalAabb.Min[i];
        } while (aabbValue <= originalAabbValue);

        codec._xyz[i] = value;
    }

    public static void ExportAabbs(this hknpStaticCompoundShape shape, ObjWriter builder, string name)
    {
        Box3 aabb = new (new Vector3(shape._boundingVolumeData._aabbTree._domain._min), new Vector3(shape._boundingVolumeData._aabbTree._domain._max));
        if (shape._boundingVolumeData._aabbTree._nodes.Count == 0)
        {
            return;
        }

        aabb.CreateAabb($"{name}-domain", builder);

        int index = 0;
        ParseNode(shape._boundingVolumeData._aabbTree._nodes[index++], aabb, builder, () => shape._boundingVolumeData._aabbTree._nodes[index++], name);
    }

    private static List<int> stored = new();

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
            stored.Add(data);
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