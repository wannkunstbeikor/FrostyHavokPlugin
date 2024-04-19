using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpConeLimitConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public byte _isEnabled;
    public byte _twistAxisInA;
    public byte _refAxisInB;
    public hkpConeLimitConstraintAtom_MeasurementMode _angleMeasurementMode;
    public byte _memOffsetToAngleOffset;
    public float _minAngle;
    public float _maxAngle;
    public float _angularLimitsTauFactor;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadByte();
        _twistAxisInA = br.ReadByte();
        _refAxisInB = br.ReadByte();
        _angleMeasurementMode = (hkpConeLimitConstraintAtom_MeasurementMode)br.ReadByte();
        _memOffsetToAngleOffset = br.ReadByte();
        br.Position += 1; // padding
        _minAngle = br.ReadSingle();
        _maxAngle = br.ReadSingle();
        _angularLimitsTauFactor = br.ReadSingle();
        br.Position += 12; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_isEnabled);
        bw.WriteByte(_twistAxisInA);
        bw.WriteByte(_refAxisInB);
        bw.WriteByte((byte)_angleMeasurementMode);
        bw.WriteByte(_memOffsetToAngleOffset);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_minAngle);
        bw.WriteSingle(_maxAngle);
        bw.WriteSingle(_angularLimitsTauFactor);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_twistAxisInA), _twistAxisInA);
        xs.WriteNumber(xe, nameof(_refAxisInB), _refAxisInB);
        xs.WriteEnum<hkpConeLimitConstraintAtom_MeasurementMode, byte>(xe, nameof(_angleMeasurementMode), (byte)_angleMeasurementMode);
        xs.WriteNumber(xe, nameof(_memOffsetToAngleOffset), _memOffsetToAngleOffset);
        xs.WriteFloat(xe, nameof(_minAngle), _minAngle);
        xs.WriteFloat(xe, nameof(_maxAngle), _maxAngle);
        xs.WriteFloat(xe, nameof(_angularLimitsTauFactor), _angularLimitsTauFactor);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpConeLimitConstraintAtom other && base.Equals(other) && _isEnabled == other._isEnabled && _twistAxisInA == other._twistAxisInA && _refAxisInB == other._refAxisInB && _angleMeasurementMode == other._angleMeasurementMode && _memOffsetToAngleOffset == other._memOffsetToAngleOffset && _minAngle == other._minAngle && _maxAngle == other._maxAngle && _angularLimitsTauFactor == other._angularLimitsTauFactor && Signature == other.Signature;
    }
    public static bool operator ==(hkpConeLimitConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpConeLimitConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_twistAxisInA);
        code.Add(_refAxisInB);
        code.Add(_angleMeasurementMode);
        code.Add(_memOffsetToAngleOffset);
        code.Add(_minAngle);
        code.Add(_maxAngle);
        code.Add(_angularLimitsTauFactor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
