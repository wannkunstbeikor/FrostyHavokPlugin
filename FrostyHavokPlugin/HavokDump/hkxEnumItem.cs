using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxEnumItem : IHavokObject
{
    public virtual uint Signature => 0;
    public int _value;
    public string _name = string.Empty;
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
        return obj is hkxEnumItem other && _value == other._value && _name == other._name && Signature == other.Signature;
    }
    public static bool operator ==(hkxEnumItem? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxEnumItem? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
