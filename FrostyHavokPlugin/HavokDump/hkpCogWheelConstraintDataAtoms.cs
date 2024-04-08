using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpCogWheelConstraintDataAtoms : IHavokObject, IEquatable<hkpCogWheelConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpCogWheelConstraintAtom _cogWheels;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _cogWheels = new hkpCogWheelConstraintAtom();
        _cogWheels.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _cogWheels.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_cogWheels), _cogWheels);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpCogWheelConstraintDataAtoms);
    }
    public bool Equals(hkpCogWheelConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _cogWheels.Equals(other._cogWheels) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_cogWheels);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
