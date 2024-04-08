using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkArrayTypeAttribute : IHavokObject, IEquatable<hkArrayTypeAttribute?>
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
        return Equals(obj as hkArrayTypeAttribute);
    }
    public bool Equals(hkArrayTypeAttribute? other)
    {
        return other is not null && _type.Equals(other._type) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
