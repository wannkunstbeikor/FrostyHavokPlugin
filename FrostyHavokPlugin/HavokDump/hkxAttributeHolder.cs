using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAttributeHolder : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hkxAttributeGroup?> _attributeGroups = new();
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
        return obj is hkxAttributeHolder other && base.Equals(other) && _attributeGroups.SequenceEqual(other._attributeGroups) && Signature == other.Signature;
    }
    public static bool operator ==(hkxAttributeHolder? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxAttributeHolder? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_attributeGroups);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
