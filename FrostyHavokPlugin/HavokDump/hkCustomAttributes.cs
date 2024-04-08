using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkCustomAttributes : IHavokObject, IEquatable<hkCustomAttributes?>
{
    public virtual uint Signature => 0;
    public List<hkCustomAttributesAttribute> _attributes;
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
        return Equals(obj as hkCustomAttributes);
    }
    public bool Equals(hkCustomAttributes? other)
    {
        return other is not null && _attributes.Equals(other._attributes) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_attributes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
