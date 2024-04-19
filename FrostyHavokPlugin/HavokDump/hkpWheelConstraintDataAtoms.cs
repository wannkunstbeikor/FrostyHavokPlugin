using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWheelConstraintDataAtoms : IHavokObject
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom? _suspensionBase;
    public hkpLinLimitConstraintAtom? _lin0Limit;
    public hkpLinSoftConstraintAtom? _lin0Soft;
    public hkpLinConstraintAtom? _lin1;
    public hkpLinConstraintAtom? _lin2;
    public hkpSetLocalRotationsConstraintAtom? _steeringBase;
    public hkp2dAngConstraintAtom? _2dAng;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _suspensionBase = new hkpSetLocalTransformsConstraintAtom();
        _suspensionBase.Read(des, br);
        _lin0Limit = new hkpLinLimitConstraintAtom();
        _lin0Limit.Read(des, br);
        _lin0Soft = new hkpLinSoftConstraintAtom();
        _lin0Soft.Read(des, br);
        _lin1 = new hkpLinConstraintAtom();
        _lin1.Read(des, br);
        _lin2 = new hkpLinConstraintAtom();
        _lin2.Read(des, br);
        _steeringBase = new hkpSetLocalRotationsConstraintAtom();
        _steeringBase.Read(des, br);
        _2dAng = new hkp2dAngConstraintAtom();
        _2dAng.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _suspensionBase.Write(s, bw);
        _lin0Limit.Write(s, bw);
        _lin0Soft.Write(s, bw);
        _lin1.Write(s, bw);
        _lin2.Write(s, bw);
        _steeringBase.Write(s, bw);
        _2dAng.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_suspensionBase), _suspensionBase);
        xs.WriteClass(xe, nameof(_lin0Limit), _lin0Limit);
        xs.WriteClass(xe, nameof(_lin0Soft), _lin0Soft);
        xs.WriteClass(xe, nameof(_lin1), _lin1);
        xs.WriteClass(xe, nameof(_lin2), _lin2);
        xs.WriteClass(xe, nameof(_steeringBase), _steeringBase);
        xs.WriteClass(xe, nameof(_2dAng), _2dAng);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpWheelConstraintDataAtoms other && _suspensionBase == other._suspensionBase && _lin0Limit == other._lin0Limit && _lin0Soft == other._lin0Soft && _lin1 == other._lin1 && _lin2 == other._lin2 && _steeringBase == other._steeringBase && _2dAng == other._2dAng && Signature == other.Signature;
    }
    public static bool operator ==(hkpWheelConstraintDataAtoms? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpWheelConstraintDataAtoms? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_suspensionBase);
        code.Add(_lin0Limit);
        code.Add(_lin0Soft);
        code.Add(_lin1);
        code.Add(_lin2);
        code.Add(_steeringBase);
        code.Add(_2dAng);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
