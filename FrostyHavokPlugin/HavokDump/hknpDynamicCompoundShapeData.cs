using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDynamicCompoundShapeData : hkReferencedObject, IEquatable<hknpDynamicCompoundShapeData?>
{
    public override uint Signature => 0;
    public hknpDynamicCompoundShapeTree _aabbTree;
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
        return Equals(obj as hknpDynamicCompoundShapeData);
    }
    public bool Equals(hknpDynamicCompoundShapeData? other)
    {
        return other is not null && _aabbTree.Equals(other._aabbTree) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabbTree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
