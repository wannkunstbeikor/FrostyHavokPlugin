using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPulleyConstraintDataAtoms : IHavokObject, IEquatable<hkpPulleyConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTranslationsConstraintAtom _translations;
    public hkpPulleyConstraintAtom _pulley;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _translations = new hkpSetLocalTranslationsConstraintAtom();
        _translations.Read(des, br);
        _pulley = new hkpPulleyConstraintAtom();
        _pulley.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _translations.Write(s, bw);
        _pulley.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_translations), _translations);
        xs.WriteClass(xe, nameof(_pulley), _pulley);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPulleyConstraintDataAtoms);
    }
    public bool Equals(hkpPulleyConstraintDataAtoms? other)
    {
        return other is not null && _translations.Equals(other._translations) && _pulley.Equals(other._pulley) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_translations);
        code.Add(_pulley);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
