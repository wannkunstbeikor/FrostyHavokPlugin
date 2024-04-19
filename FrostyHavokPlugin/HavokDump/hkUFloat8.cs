using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkUFloat8 : IHavokObject
{
    public virtual uint Signature => 0;
    public byte _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _value = br.ReadByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteByte(_value);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkUFloat8 other && _value == other._value && Signature == other.Signature;
    }
    public static bool operator ==(hkUFloat8? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkUFloat8? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
