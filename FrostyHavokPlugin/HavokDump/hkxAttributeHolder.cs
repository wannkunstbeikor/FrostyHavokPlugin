using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAttributeHolder : hkReferencedObject, IEquatable<hkxAttributeHolder?>
{
    public override uint Signature => 0;
    public List<hkxAttributeGroup> _attributeGroups;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _attributeGroups = des.ReadClassArray<hkxAttributeGroup>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkxAttributeGroup>(bw, _attributeGroups);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkxAttributeGroup>(xe, nameof(_attributeGroups), _attributeGroups);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxAttributeHolder);
    }
    public bool Equals(hkxAttributeHolder? other)
    {
        return other is not null && _attributeGroups.Equals(other._attributeGroups) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_attributeGroups);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
