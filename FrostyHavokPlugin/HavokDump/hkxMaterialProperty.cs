using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterialProperty : IHavokObject, IEquatable<hkxMaterialProperty?>
{
    public virtual uint Signature => 0;
    public uint _key;
    public uint _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _key = br.ReadUInt32();
        _value = br.ReadUInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_key);
        bw.WriteUInt32(_value);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_key), _key);
        xs.WriteNumber(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxMaterialProperty);
    }
    public bool Equals(hkxMaterialProperty? other)
    {
        return other is not null && _key.Equals(other._key) && _value.Equals(other._value) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_key);
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
