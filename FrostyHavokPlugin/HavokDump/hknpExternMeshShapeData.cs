using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpExternMeshShapeData : hkReferencedObject
{
    public override uint Signature => 0;
    public hkcdStaticTreeDefaultTreeStorage6? _aabbTree;
    public hkcdSimdTree? _simdTree;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _aabbTree = new hkcdStaticTreeDefaultTreeStorage6();
        _aabbTree.Read(des, br);
        _simdTree = new hkcdSimdTree();
        _simdTree.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _aabbTree.Write(s, bw);
        _simdTree.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_aabbTree), _aabbTree);
        xs.WriteClass(xe, nameof(_simdTree), _simdTree);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpExternMeshShapeData other && base.Equals(other) && _aabbTree == other._aabbTree && _simdTree == other._simdTree && Signature == other.Signature;
    }
    public static bool operator ==(hknpExternMeshShapeData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpExternMeshShapeData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabbTree);
        code.Add(_simdTree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
