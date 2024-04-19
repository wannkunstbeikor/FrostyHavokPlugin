using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkArrayTypeAttribute : IHavokObject
{
    public virtual uint Signature => 0;
    public hkArrayTypeAttribute_ArrayType _type;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _type = (hkArrayTypeAttribute_ArrayType)br.ReadSByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSByte((sbyte)_type);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteEnum<hkArrayTypeAttribute_ArrayType, sbyte>(xe, nameof(_type), (sbyte)_type);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkArrayTypeAttribute other && _type == other._type && Signature == other.Signature;
    }
    public static bool operator ==(hkArrayTypeAttribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkArrayTypeAttribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
