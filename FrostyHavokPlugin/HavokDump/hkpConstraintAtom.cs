using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpConstraintAtom : IHavokObject, IEquatable<hkpConstraintAtom?>
{
    public virtual uint Signature => 0;
    public hkpConstraintAtom_AtomType _type;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _type = (hkpConstraintAtom_AtomType)br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt16((ushort)_type);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteEnum<hkpConstraintAtom_AtomType, ushort>(xe, nameof(_type), (ushort)_type);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpConstraintAtom);
    }
    public bool Equals(hkpConstraintAtom? other)
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
