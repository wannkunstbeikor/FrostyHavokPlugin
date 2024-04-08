using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPrismaticConstraintDataAtoms : IHavokObject, IEquatable<hkpPrismaticConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpLinMotorConstraintAtom _motor;
    public hkpLinFrictionConstraintAtom _friction;
    public hkpAngConstraintAtom _ang;
    public hkpLinConstraintAtom _lin0;
    public hkpLinConstraintAtom _lin1;
    public hkpLinLimitConstraintAtom _linLimit;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _motor = new hkpLinMotorConstraintAtom();
        _motor.Read(des, br);
        _friction = new hkpLinFrictionConstraintAtom();
        _friction.Read(des, br);
        _ang = new hkpAngConstraintAtom();
        _ang.Read(des, br);
        _lin0 = new hkpLinConstraintAtom();
        _lin0.Read(des, br);
        _lin1 = new hkpLinConstraintAtom();
        _lin1.Read(des, br);
        _linLimit = new hkpLinLimitConstraintAtom();
        _linLimit.Read(des, br);
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _motor.Write(s, bw);
        _friction.Write(s, bw);
        _ang.Write(s, bw);
        _lin0.Write(s, bw);
        _lin1.Write(s, bw);
        _linLimit.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_motor), _motor);
        xs.WriteClass(xe, nameof(_friction), _friction);
        xs.WriteClass(xe, nameof(_ang), _ang);
        xs.WriteClass(xe, nameof(_lin0), _lin0);
        xs.WriteClass(xe, nameof(_lin1), _lin1);
        xs.WriteClass(xe, nameof(_linLimit), _linLimit);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPrismaticConstraintDataAtoms);
    }
    public bool Equals(hkpPrismaticConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _motor.Equals(other._motor) && _friction.Equals(other._friction) && _ang.Equals(other._ang) && _lin0.Equals(other._lin0) && _lin1.Equals(other._lin1) && _linLimit.Equals(other._linLimit) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_motor);
        code.Add(_friction);
        code.Add(_ang);
        code.Add(_lin0);
        code.Add(_lin1);
        code.Add(_linLimit);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
