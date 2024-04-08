using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPulleyConstraintData : hkpConstraintData, IEquatable<hkpPulleyConstraintData?>
{
    public override uint Signature => 0;
    public hkpPulleyConstraintDataAtoms _atoms;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _atoms = new hkpPulleyConstraintDataAtoms();
        _atoms.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        _atoms.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_atoms), _atoms);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPulleyConstraintData);
    }
    public bool Equals(hkpPulleyConstraintData? other)
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
