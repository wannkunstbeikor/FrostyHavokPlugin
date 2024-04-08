using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLinearClearanceConstraintDataAtoms : IHavokObject, IEquatable<hkpLinearClearanceConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpLinMotorConstraintAtom _motor;
    public hkpLinFrictionConstraintAtom _friction0;
    public hkpLinFrictionConstraintAtom _friction1;
    public hkpLinFrictionConstraintAtom _friction2;
    public hkpAngConstraintAtom _ang;
    public hkpLinLimitConstraintAtom _linLimit0;
    public hkpLinLimitConstraintAtom _linLimit1;
    public hkpLinLimitConstraintAtom _linLimit2;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _motor = new hkpLinMotorConstraintAtom();
        _motor.Read(des, br);
        _friction0 = new hkpLinFrictionConstraintAtom();
        _friction0.Read(des, br);
        _friction1 = new hkpLinFrictionConstraintAtom();
        _friction1.Read(des, br);
        _friction2 = new hkpLinFrictionConstraintAtom();
        _friction2.Read(des, br);
        _ang = new hkpAngConstraintAtom();
        _ang.Read(des, br);
        _linLimit0 = new hkpLinLimitConstraintAtom();
        _linLimit0.Read(des, br);
        _linLimit1 = new hkpLinLimitConstraintAtom();
        _linLimit1.Read(des, br);
        _linLimit2 = new hkpLinLimitConstraintAtom();
        _linLimit2.Read(des, br);
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _motor.Write(s, bw);
        _friction0.Write(s, bw);
        _friction1.Write(s, bw);
        _friction2.Write(s, bw);
        _ang.Write(s, bw);
        _linLimit0.Write(s, bw);
        _linLimit1.Write(s, bw);
        _linLimit2.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_motor), _motor);
        xs.WriteClass(xe, nameof(_friction0), _friction0);
        xs.WriteClass(xe, nameof(_friction1), _friction1);
        xs.WriteClass(xe, nameof(_friction2), _friction2);
        xs.WriteClass(xe, nameof(_ang), _ang);
        xs.WriteClass(xe, nameof(_linLimit0), _linLimit0);
        xs.WriteClass(xe, nameof(_linLimit1), _linLimit1);
        xs.WriteClass(xe, nameof(_linLimit2), _linLimit2);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpLinearClearanceConstraintDataAtoms);
    }
    public bool Equals(hkpLinearClearanceConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _motor.Equals(other._motor) && _friction0.Equals(other._friction0) && _friction1.Equals(other._friction1) && _friction2.Equals(other._friction2) && _ang.Equals(other._ang) && _linLimit0.Equals(other._linLimit0) && _linLimit1.Equals(other._linLimit1) && _linLimit2.Equals(other._linLimit2) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_motor);
        code.Add(_friction0);
        code.Add(_friction1);
        code.Add(_friction2);
        code.Add(_ang);
        code.Add(_linLimit0);
        code.Add(_linLimit1);
        code.Add(_linLimit2);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
