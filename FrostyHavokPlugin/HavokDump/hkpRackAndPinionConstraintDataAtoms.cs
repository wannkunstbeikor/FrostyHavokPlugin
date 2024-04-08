using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRackAndPinionConstraintDataAtoms : IHavokObject, IEquatable<hkpRackAndPinionConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpRackAndPinionConstraintAtom _rackAndPinion;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _rackAndPinion = new hkpRackAndPinionConstraintAtom();
        _rackAndPinion.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _rackAndPinion.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_rackAndPinion), _rackAndPinion);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpRackAndPinionConstraintDataAtoms);
    }
    public bool Equals(hkpRackAndPinionConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _rackAndPinion.Equals(other._rackAndPinion) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_rackAndPinion);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
