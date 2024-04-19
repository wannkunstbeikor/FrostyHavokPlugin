using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWheelFrictionConstraintDataAtoms : IHavokObject
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom? _transforms;
    public hkpWheelFrictionConstraintAtom? _friction;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _friction = new hkpWheelFrictionConstraintAtom();
        _friction.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _friction.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_friction), _friction);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpWheelFrictionConstraintDataAtoms other && _transforms == other._transforms && _friction == other._friction && Signature == other.Signature;
    }
    public static bool operator ==(hkpWheelFrictionConstraintDataAtoms? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpWheelFrictionConstraintDataAtoms? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_friction);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
