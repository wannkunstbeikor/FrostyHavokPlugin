using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPointToPlaneConstraintData : hkpConstraintData, IEquatable<hkpPointToPlaneConstraintData?>
{
    public override uint Signature => 0;
    public hkpPointToPlaneConstraintDataAtoms _atoms;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _atoms = new hkpPointToPlaneConstraintDataAtoms();
        _atoms.Read(des, br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_atoms), _atoms);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPointToPlaneConstraintData);
    }
    public bool Equals(hkpPointToPlaneConstraintData? other)
    {
        return other is not null && _atoms.Equals(other._atoms) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_atoms);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
