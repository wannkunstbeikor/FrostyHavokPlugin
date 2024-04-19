using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLimitedForceConstraintMotor : hkpConstraintMotor
{
    public override uint Signature => 0;
    public float _minForce;
    public float _maxForce;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _minForce = br.ReadSingle();
        _maxForce = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_minForce);
        bw.WriteSingle(_maxForce);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_minForce), _minForce);
        xs.WriteFloat(xe, nameof(_maxForce), _maxForce);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpLimitedForceConstraintMotor other && base.Equals(other) && _minForce == other._minForce && _maxForce == other._maxForce && Signature == other.Signature;
    }
    public static bool operator ==(hkpLimitedForceConstraintMotor? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpLimitedForceConstraintMotor? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_minForce);
        code.Add(_maxForce);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
