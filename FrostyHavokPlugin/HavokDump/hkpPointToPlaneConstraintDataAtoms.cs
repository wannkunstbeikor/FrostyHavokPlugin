using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPointToPlaneConstraintDataAtoms : IHavokObject, IEquatable<hkpPointToPlaneConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpLinConstraintAtom _lin;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _lin = new hkpLinConstraintAtom();
        _lin.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _lin.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_lin), _lin);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPointToPlaneConstraintDataAtoms);
    }
    public bool Equals(hkpPointToPlaneConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _lin.Equals(other._lin) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_lin);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
