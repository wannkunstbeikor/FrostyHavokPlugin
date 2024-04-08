using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMaterialPalette : hkReferencedObject, IEquatable<hknpMaterialPalette?>
{
    public override uint Signature => 0;
    public List<hknpMaterialDescriptor> _entries;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _entries = des.ReadClassArray<hknpMaterialDescriptor>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hknpMaterialDescriptor>(bw, _entries);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hknpMaterialDescriptor>(xe, nameof(_entries), _entries);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpMaterialPalette);
    }
    public bool Equals(hknpMaterialPalette? other)
    {
        return other is not null && _entries.Equals(other._entries) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_entries);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
