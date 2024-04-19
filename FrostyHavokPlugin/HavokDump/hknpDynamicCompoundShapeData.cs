using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDynamicCompoundShapeData : hkReferencedObject
{
    public override uint Signature => 0;
    public hknpDynamicCompoundShapeTree? _aabbTree;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _aabbTree = new hknpDynamicCompoundShapeTree();
        _aabbTree.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _aabbTree.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_aabbTree), _aabbTree);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpDynamicCompoundShapeData other && base.Equals(other) && _aabbTree == other._aabbTree && Signature == other.Signature;
    }
    public static bool operator ==(hknpDynamicCompoundShapeData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpDynamicCompoundShapeData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabbTree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
