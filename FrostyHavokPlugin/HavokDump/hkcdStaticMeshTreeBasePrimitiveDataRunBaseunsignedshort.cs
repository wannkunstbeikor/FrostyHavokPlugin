using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBasePrimitiveDataRunBaseunsignedshort : IHavokObject
{
    public virtual uint Signature => 0;
    public ushort _value;
    public byte _index;
    public byte _count;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _value = br.ReadUInt16();
        _index = br.ReadByte();
        _count = br.ReadByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt16(_value);
        bw.WriteByte(_index);
        bw.WriteByte(_count);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_value), _value);
        xs.WriteNumber(xe, nameof(_index), _index);
        xs.WriteNumber(xe, nameof(_count), _count);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticMeshTreeBasePrimitiveDataRunBaseunsignedshort other && _value == other._value && _index == other._index && _count == other._count && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticMeshTreeBasePrimitiveDataRunBaseunsignedshort? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticMeshTreeBasePrimitiveDataRunBaseunsignedshort? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(_index);
        code.Add(_count);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
