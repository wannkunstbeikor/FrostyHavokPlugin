using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAttribute : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public hkReferencedObject? _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _value = des.ReadClassPointer<hkReferencedObject>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        s.WriteClassPointer<hkReferencedObject>(bw, _value);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointer(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxAttribute other && _name == other._name && _value == other._value && Signature == other.Signature;
    }
    public static bool operator ==(hkxAttribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxAttribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
