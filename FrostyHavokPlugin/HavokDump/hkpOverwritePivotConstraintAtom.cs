using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpOverwritePivotConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public byte _copyToPivotBFromPivotA;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _copyToPivotBFromPivotA = br.ReadByte();
        br.Position += 13; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_copyToPivotBFromPivotA);
        for (int i = 0; i < 13; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_copyToPivotBFromPivotA), _copyToPivotBFromPivotA);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpOverwritePivotConstraintAtom other && base.Equals(other) && _copyToPivotBFromPivotA == other._copyToPivotBFromPivotA && Signature == other.Signature;
    }
    public static bool operator ==(hkpOverwritePivotConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpOverwritePivotConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_copyToPivotBFromPivotA);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
