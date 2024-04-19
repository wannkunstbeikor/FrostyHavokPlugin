using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkIntRealPair : IHavokObject
{
    public virtual uint Signature => 0;
    public int _key;
    public float _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _key = br.ReadInt32();
        _value = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_key);
        bw.WriteSingle(_value);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_key), _key);
        xs.WriteFloat(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkIntRealPair other && _key == other._key && _value == other._value && Signature == other.Signature;
    }
    public static bool operator ==(hkIntRealPair? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkIntRealPair? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_key);
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
