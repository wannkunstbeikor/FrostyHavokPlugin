using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBridgeConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public int _numSolverResults;
    public hkpConstraintData? _constraintData;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 2; // padding
        _numSolverResults = br.ReadInt32();
        _constraintData = des.ReadClassPointer<hkpConstraintData>(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numSolverResults);
        s.WriteClassPointer<hkpConstraintData>(bw, _constraintData);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_numSolverResults), _numSolverResults);
        xs.WriteClassPointer(xe, nameof(_constraintData), _constraintData);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpBridgeConstraintAtom other && base.Equals(other) && _numSolverResults == other._numSolverResults && _constraintData == other._constraintData && Signature == other.Signature;
    }
    public static bool operator ==(hknpBridgeConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpBridgeConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_numSolverResults);
        code.Add(_constraintData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
