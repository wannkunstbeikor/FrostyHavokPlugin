using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpTwistLimitConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public byte _isEnabled;
    public byte _twistAxis;
    public byte _refAxis;
    public float _minAngle;
    public float _maxAngle;
    public float _angularLimitsTauFactor;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadByte();
        _twistAxis = br.ReadByte();
        _refAxis = br.ReadByte();
        br.Position += 3; // padding
        _minAngle = br.ReadSingle();
        _maxAngle = br.ReadSingle();
        _angularLimitsTauFactor = br.ReadSingle();
        br.Position += 12; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_isEnabled);
        bw.WriteByte(_twistAxis);
        bw.WriteByte(_refAxis);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_minAngle);
        bw.WriteSingle(_maxAngle);
        bw.WriteSingle(_angularLimitsTauFactor);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_twistAxis), _twistAxis);
        xs.WriteNumber(xe, nameof(_refAxis), _refAxis);
        xs.WriteFloat(xe, nameof(_minAngle), _minAngle);
        xs.WriteFloat(xe, nameof(_maxAngle), _maxAngle);
        xs.WriteFloat(xe, nameof(_angularLimitsTauFactor), _angularLimitsTauFactor);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpTwistLimitConstraintAtom other && base.Equals(other) && _isEnabled == other._isEnabled && _twistAxis == other._twistAxis && _refAxis == other._refAxis && _minAngle == other._minAngle && _maxAngle == other._maxAngle && _angularLimitsTauFactor == other._angularLimitsTauFactor && Signature == other.Signature;
    }
    public static bool operator ==(hkpTwistLimitConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpTwistLimitConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_twistAxis);
        code.Add(_refAxis);
        code.Add(_minAngle);
        code.Add(_maxAngle);
        code.Add(_angularLimitsTauFactor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
