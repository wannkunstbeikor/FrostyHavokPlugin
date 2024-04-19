using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpConstraintAtom : IHavokObject
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
        return obj is hkpConstraintAtom other && _type == other._type && Signature == other.Signature;
    }
    public static bool operator ==(hkpConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
