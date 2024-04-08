using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkClassEnumItem : IHavokObject, IEquatable<hkClassEnumItem?>
{
    public virtual uint Signature => 0;
    public int _value;
    public string _name;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _value = br.ReadInt32();
        br.Position += 4; // padding
        _name = des.ReadStringPointer(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_value);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteStringPointer(bw, _name);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_value), _value);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkClassEnumItem);
    }
    public bool Equals(hkClassEnumItem? other)
    {
        return other is not null && _value.Equals(other._value) && _name.Equals(other._name) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
