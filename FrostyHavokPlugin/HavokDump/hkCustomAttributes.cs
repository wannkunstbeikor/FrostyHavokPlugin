using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkCustomAttributes : IHavokObject
{
    public virtual uint Signature => 0;
    public List<hkCustomAttributesAttribute?> _attributes = new();
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        // Read TYPE_SIMPLEARRAY
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        // Write TYPE_SIMPLEARRAY
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        // Write TYPE_SIMPLEARRAY
    }
    public override bool Equals(object? obj)
    {
        return obj is hkCustomAttributes other && _attributes.SequenceEqual(other._attributes) && Signature == other.Signature;
    }
    public static bool operator ==(hkCustomAttributes? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkCustomAttributes? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_attributes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
