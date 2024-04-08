using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkFloat16 : IHavokObject, IEquatable<hkFloat16?>
{
    public virtual uint Signature => 0;
    public ushort _value;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _value = br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt16(_value);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkFloat16);
    }
    public bool Equals(hkFloat16? other)
    {
        return other is not null && _value.Equals(other._value) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
