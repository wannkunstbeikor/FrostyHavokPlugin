using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpStiffSpringConstraintDataAtoms : IHavokObject, IEquatable<hkpStiffSpringConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTranslationsConstraintAtom _pivots;
    public hkpSetupStabilizationAtom _setupStabilization;
    public hkpStiffSpringConstraintAtom _spring;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _pivots = new hkpSetLocalTranslationsConstraintAtom();
        _pivots.Read(des, br);
        _setupStabilization = new hkpSetupStabilizationAtom();
        _setupStabilization.Read(des, br);
        _spring = new hkpStiffSpringConstraintAtom();
        _spring.Read(des, br);
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _pivots.Write(s, bw);
        _setupStabilization.Write(s, bw);
        _spring.Write(s, bw);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_pivots), _pivots);
        xs.WriteClass(xe, nameof(_setupStabilization), _setupStabilization);
        xs.WriteClass(xe, nameof(_spring), _spring);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpStiffSpringConstraintDataAtoms);
    }
    public bool Equals(hkpStiffSpringConstraintDataAtoms? other)
    {
        return other is not null && _pivots.Equals(other._pivots) && _setupStabilization.Equals(other._setupStabilization) && _spring.Equals(other._spring) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_pivots);
        code.Add(_setupStabilization);
        code.Add(_spring);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
