using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRackAndPinionConstraintAtom : hkpConstraintAtom, IEquatable<hkpRackAndPinionConstraintAtom?>
{
    public override uint Signature => 0;
    public float _pinionRadiusOrScrewPitch;
    public bool _isScrew;
    public sbyte _memOffsetToInitialAngleOffset;
    public sbyte _memOffsetToPrevAngle;
    public sbyte _memOffsetToRevolutionCounter;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 2; // padding
        _pinionRadiusOrScrewPitch = br.ReadSingle();
        _isScrew = br.ReadBoolean();
        _memOffsetToInitialAngleOffset = br.ReadSByte();
        _memOffsetToPrevAngle = br.ReadSByte();
        _memOffsetToRevolutionCounter = br.ReadSByte();
        br.Position += 4; // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_pinionRadiusOrScrewPitch), _pinionRadiusOrScrewPitch);
        xs.WriteBoolean(xe, nameof(_isScrew), _isScrew);
        xs.WriteNumber(xe, nameof(_memOffsetToInitialAngleOffset), _memOffsetToInitialAngleOffset);
        xs.WriteNumber(xe, nameof(_memOffsetToPrevAngle), _memOffsetToPrevAngle);
        xs.WriteNumber(xe, nameof(_memOffsetToRevolutionCounter), _memOffsetToRevolutionCounter);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpRackAndPinionConstraintAtom);
    }
    public bool Equals(hkpRackAndPinionConstraintAtom? other)
    {
        return other is not null && _pinionRadiusOrScrewPitch.Equals(other._pinionRadiusOrScrewPitch) && _isScrew.Equals(other._isScrew) && _memOffsetToInitialAngleOffset.Equals(other._memOffsetToInitialAngleOffset) && _memOffsetToPrevAngle.Equals(other._memOffsetToPrevAngle) && _memOffsetToRevolutionCounter.Equals(other._memOffsetToRevolutionCounter) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_pinionRadiusOrScrewPitch);
        code.Add(_isScrew);
        code.Add(_memOffsetToInitialAngleOffset);
        code.Add(_memOffsetToPrevAngle);
        code.Add(_memOffsetToRevolutionCounter);
        code.Add(Signature);
        return code.ToHashCode();
    }
}