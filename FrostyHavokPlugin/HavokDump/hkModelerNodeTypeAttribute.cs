using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkModelerNodeTypeAttribute : IHavokObject, IEquatable<hkModelerNodeTypeAttribute?>
{
    public virtual uint Signature => 0;
    public hkModelerNodeTypeAttribute_ModelerType _type;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _type = (hkModelerNodeTypeAttribute_ModelerType)br.ReadSByte();
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteEnum<hkModelerNodeTypeAttribute_ModelerType, sbyte>(xe, nameof(_type), (sbyte)_type);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkModelerNodeTypeAttribute);
    }
    public bool Equals(hkModelerNodeTypeAttribute? other)
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
