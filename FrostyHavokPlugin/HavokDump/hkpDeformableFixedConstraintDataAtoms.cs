using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpDeformableFixedConstraintDataAtoms : IHavokObject, IEquatable<hkpDeformableFixedConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpDeformableLinConstraintAtom _lin;
    public hkpDeformableAngConstraintAtom _ang;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _lin = new hkpDeformableLinConstraintAtom();
        _lin.Read(des, br);
        _ang = new hkpDeformableAngConstraintAtom();
        _ang.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _lin.Write(s, bw);
        _ang.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_lin), _lin);
        xs.WriteClass(xe, nameof(_ang), _ang);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpDeformableFixedConstraintDataAtoms);
    }
    public bool Equals(hkpDeformableFixedConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _lin.Equals(other._lin) && _ang.Equals(other._ang) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_lin);
        code.Add(_ang);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
