using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWheelFrictionConstraintDataAtoms : IHavokObject, IEquatable<hkpWheelFrictionConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpWheelFrictionConstraintAtom _friction;
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
        return Equals(obj as hkpWheelFrictionConstraintDataAtoms);
    }
    public bool Equals(hkpWheelFrictionConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _friction.Equals(other._friction) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_friction);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
