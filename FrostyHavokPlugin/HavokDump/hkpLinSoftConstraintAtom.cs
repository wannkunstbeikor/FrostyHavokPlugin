using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLinSoftConstraintAtom : hkpConstraintAtom, IEquatable<hkpLinSoftConstraintAtom?>
{
    public override uint Signature => 0;
    public byte _axisIndex;
    public float _tau;
    public float _damping;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _axisIndex = br.ReadByte();
        br.Position += 1; // padding
        _tau = br.ReadSingle();
        _damping = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_axisIndex);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_tau);
        bw.WriteSingle(_damping);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_axisIndex), _axisIndex);
        xs.WriteFloat(xe, nameof(_tau), _tau);
        xs.WriteFloat(xe, nameof(_damping), _damping);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpLinSoftConstraintAtom);
    }
    public bool Equals(hkpLinSoftConstraintAtom? other)
    {
        return other is not null && _axisIndex.Equals(other._axisIndex) && _tau.Equals(other._tau) && _damping.Equals(other._damping) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_axisIndex);
        code.Add(_tau);
        code.Add(_damping);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
