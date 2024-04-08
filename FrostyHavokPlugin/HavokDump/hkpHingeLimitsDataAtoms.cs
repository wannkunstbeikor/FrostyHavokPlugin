using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpHingeLimitsDataAtoms : IHavokObject, IEquatable<hkpHingeLimitsDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalRotationsConstraintAtom _rotations;
    public hkpAngLimitConstraintAtom _angLimit;
    public hkp2dAngConstraintAtom _2dAng;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _rotations = new hkpSetLocalRotationsConstraintAtom();
        _rotations.Read(des, br);
        _angLimit = new hkpAngLimitConstraintAtom();
        _angLimit.Read(des, br);
        _2dAng = new hkp2dAngConstraintAtom();
        _2dAng.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _rotations.Write(s, bw);
        _angLimit.Write(s, bw);
        _2dAng.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_rotations), _rotations);
        xs.WriteClass(xe, nameof(_angLimit), _angLimit);
        xs.WriteClass(xe, nameof(_2dAng), _2dAng);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpHingeLimitsDataAtoms);
    }
    public bool Equals(hkpHingeLimitsDataAtoms? other)
    {
        return other is not null && _rotations.Equals(other._rotations) && _angLimit.Equals(other._angLimit) && _2dAng.Equals(other._2dAng) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_rotations);
        code.Add(_angLimit);
        code.Add(_2dAng);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
