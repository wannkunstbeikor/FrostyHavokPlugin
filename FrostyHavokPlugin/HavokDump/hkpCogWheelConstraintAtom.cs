using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpCogWheelConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public float _cogWheelRadiusA;
    public float _cogWheelRadiusB;
    public bool _isScrew;
    public sbyte _memOffsetToInitialAngleOffset;
    public sbyte _memOffsetToPrevAngle;
    public sbyte _memOffsetToRevolutionCounter;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 2; // padding
        _cogWheelRadiusA = br.ReadSingle();
        _cogWheelRadiusB = br.ReadSingle();
        _isScrew = br.ReadBoolean();
        _memOffsetToInitialAngleOffset = br.ReadSByte();
        _memOffsetToPrevAngle = br.ReadSByte();
        _memOffsetToRevolutionCounter = br.ReadSByte();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_cogWheelRadiusA);
        bw.WriteSingle(_cogWheelRadiusB);
        bw.WriteBoolean(_isScrew);
        bw.WriteSByte(_memOffsetToInitialAngleOffset);
        bw.WriteSByte(_memOffsetToPrevAngle);
        bw.WriteSByte(_memOffsetToRevolutionCounter);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_cogWheelRadiusA), _cogWheelRadiusA);
        xs.WriteFloat(xe, nameof(_cogWheelRadiusB), _cogWheelRadiusB);
        xs.WriteBoolean(xe, nameof(_isScrew), _isScrew);
        xs.WriteNumber(xe, nameof(_memOffsetToInitialAngleOffset), _memOffsetToInitialAngleOffset);
        xs.WriteNumber(xe, nameof(_memOffsetToPrevAngle), _memOffsetToPrevAngle);
        xs.WriteNumber(xe, nameof(_memOffsetToRevolutionCounter), _memOffsetToRevolutionCounter);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpCogWheelConstraintAtom other && base.Equals(other) && _cogWheelRadiusA == other._cogWheelRadiusA && _cogWheelRadiusB == other._cogWheelRadiusB && _isScrew == other._isScrew && _memOffsetToInitialAngleOffset == other._memOffsetToInitialAngleOffset && _memOffsetToPrevAngle == other._memOffsetToPrevAngle && _memOffsetToRevolutionCounter == other._memOffsetToRevolutionCounter && Signature == other.Signature;
    }
    public static bool operator ==(hkpCogWheelConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpCogWheelConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_cogWheelRadiusA);
        code.Add(_cogWheelRadiusB);
        code.Add(_isScrew);
        code.Add(_memOffsetToInitialAngleOffset);
        code.Add(_memOffsetToPrevAngle);
        code.Add(_memOffsetToRevolutionCounter);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
