using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkCustomAttributesAttribute : IHavokObject, IEquatable<hkCustomAttributesAttribute?>
{
    public virtual uint Signature => 0;
    public string _name;
    public ulong _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        // Read TYPE_VARIANT
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        // Write TYPE_VARIANT
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        // Write TYPE_VARIANT
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkCustomAttributesAttribute);
    }
    public bool Equals(hkCustomAttributesAttribute? other)
    {
        return other is not null && _name.Equals(other._name) && _value.Equals(other._value) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
