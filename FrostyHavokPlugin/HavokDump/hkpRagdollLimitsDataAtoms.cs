using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRagdollLimitsDataAtoms : IHavokObject
{
    public virtual uint Signature => 0;
    public hkpSetLocalRotationsConstraintAtom? _rotations;
    public hkpTwistLimitConstraintAtom? _twistLimit;
    public hkpConeLimitConstraintAtom? _coneLimit;
    public hkpConeLimitConstraintAtom? _planesLimit;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _rotations = new hkpSetLocalRotationsConstraintAtom();
        _rotations.Read(des, br);
        _twistLimit = new hkpTwistLimitConstraintAtom();
        _twistLimit.Read(des, br);
        _coneLimit = new hkpConeLimitConstraintAtom();
        _coneLimit.Read(des, br);
        _planesLimit = new hkpConeLimitConstraintAtom();
        _planesLimit.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _rotations.Write(s, bw);
        _twistLimit.Write(s, bw);
        _coneLimit.Write(s, bw);
        _planesLimit.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_rotations), _rotations);
        xs.WriteClass(xe, nameof(_twistLimit), _twistLimit);
        xs.WriteClass(xe, nameof(_coneLimit), _coneLimit);
        xs.WriteClass(xe, nameof(_planesLimit), _planesLimit);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpRagdollLimitsDataAtoms other && _rotations == other._rotations && _twistLimit == other._twistLimit && _coneLimit == other._coneLimit && _planesLimit == other._planesLimit && Signature == other.Signature;
    }
    public static bool operator ==(hkpRagdollLimitsDataAtoms? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpRagdollLimitsDataAtoms? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_rotations);
        code.Add(_twistLimit);
        code.Add(_coneLimit);
        code.Add(_planesLimit);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
