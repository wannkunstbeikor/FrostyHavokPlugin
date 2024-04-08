using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpAngConstraintAtom : hkpConstraintAtom, IEquatable<hkpAngConstraintAtom?>
{
    public override uint Signature => 0;
    public byte _firstConstrainedAxis;
    public byte _numConstrainedAxes;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _firstConstrainedAxis = br.ReadByte();
        _numConstrainedAxes = br.ReadByte();
        br.Position += 12; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_firstConstrainedAxis);
        bw.WriteByte(_numConstrainedAxes);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_firstConstrainedAxis), _firstConstrainedAxis);
        xs.WriteNumber(xe, nameof(_numConstrainedAxes), _numConstrainedAxes);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpAngConstraintAtom);
    }
    public bool Equals(hkpAngConstraintAtom? other)
    {
        return other is not null && _firstConstrainedAxis.Equals(other._firstConstrainedAxis) && _numConstrainedAxes.Equals(other._numConstrainedAxes) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_firstConstrainedAxis);
        code.Add(_numConstrainedAxes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
