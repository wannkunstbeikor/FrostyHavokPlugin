using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAttributeGroup : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public List<hkxAttribute?> _attributes = new();
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _attributes = des.ReadClassArray<hkxAttribute>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        s.WriteClassArray<hkxAttribute>(bw, _attributes);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassArray<hkxAttribute>(xe, nameof(_attributes), _attributes);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxAttributeGroup other && _name == other._name && _attributes.SequenceEqual(other._attributes) && Signature == other.Signature;
    }
    public static bool operator ==(hkxAttributeGroup? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxAttributeGroup? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_attributes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
