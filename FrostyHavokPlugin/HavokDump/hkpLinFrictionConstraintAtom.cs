using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLinFrictionConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public byte _isEnabled;
    public byte _frictionAxis;
    public float _maxFrictionForce;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadByte();
        _frictionAxis = br.ReadByte();
        _maxFrictionForce = br.ReadSingle();
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_isEnabled);
        bw.WriteByte(_frictionAxis);
        bw.WriteSingle(_maxFrictionForce);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_frictionAxis), _frictionAxis);
        xs.WriteFloat(xe, nameof(_maxFrictionForce), _maxFrictionForce);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpLinFrictionConstraintAtom other && base.Equals(other) && _isEnabled == other._isEnabled && _frictionAxis == other._frictionAxis && _maxFrictionForce == other._maxFrictionForce && Signature == other.Signature;
    }
    public static bool operator ==(hkpLinFrictionConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpLinFrictionConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_frictionAxis);
        code.Add(_maxFrictionForce);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
