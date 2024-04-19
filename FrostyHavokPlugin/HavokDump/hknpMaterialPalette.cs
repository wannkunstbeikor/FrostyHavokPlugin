using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMaterialPalette : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hknpMaterialDescriptor?> _entries = new();
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
        return obj is hknpMaterialPalette other && base.Equals(other) && _entries.SequenceEqual(other._entries) && Signature == other.Signature;
    }
    public static bool operator ==(hknpMaterialPalette? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMaterialPalette? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_entries);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
