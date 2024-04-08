using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRotationalConstraintDataAtoms : IHavokObject, IEquatable<hkpRotationalConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalRotationsConstraintAtom _rotations;
    public hkpAngConstraintAtom _ang;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _rotations = new hkpSetLocalRotationsConstraintAtom();
        _rotations.Read(des, br);
        _ang = new hkpAngConstraintAtom();
        _ang.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _rotations.Write(s, bw);
        _ang.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_rotations), _rotations);
        xs.WriteClass(xe, nameof(_ang), _ang);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpRotationalConstraintDataAtoms);
    }
    public bool Equals(hkpRotationalConstraintDataAtoms? other)
    {
        return other is not null && _rotations.Equals(other._rotations) && _ang.Equals(other._ang) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_rotations);
        code.Add(_ang);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
