using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDataWheelComponentParams : IHavokObject
{
    public virtual uint Signature => 0;
    public float _radius;
    public float _mass;
    public float _width;
    public float _friction;
    public float _viscosityFriction;
    public float _maxFriction;
    public float _slipAngle;
    public float _forceFeedbackMultiplier;
    public float _maxContactBodyAcceleration;
    public sbyte _axle;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _radius = br.ReadSingle();
        _mass = br.ReadSingle();
        _width = br.ReadSingle();
        _friction = br.ReadSingle();
        _viscosityFriction = br.ReadSingle();
        _maxFriction = br.ReadSingle();
        _slipAngle = br.ReadSingle();
        _forceFeedbackMultiplier = br.ReadSingle();
        _maxContactBodyAcceleration = br.ReadSingle();
        _axle = br.ReadSByte();
        br.Position += 3; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_radius);
        bw.WriteSingle(_mass);
        bw.WriteSingle(_width);
        bw.WriteSingle(_friction);
        bw.WriteSingle(_viscosityFriction);
        bw.WriteSingle(_maxFriction);
        bw.WriteSingle(_slipAngle);
        bw.WriteSingle(_forceFeedbackMultiplier);
        bw.WriteSingle(_maxContactBodyAcceleration);
        bw.WriteSByte(_axle);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_radius), _radius);
        xs.WriteFloat(xe, nameof(_mass), _mass);
        xs.WriteFloat(xe, nameof(_width), _width);
        xs.WriteFloat(xe, nameof(_friction), _friction);
        xs.WriteFloat(xe, nameof(_viscosityFriction), _viscosityFriction);
        xs.WriteFloat(xe, nameof(_maxFriction), _maxFriction);
        xs.WriteFloat(xe, nameof(_slipAngle), _slipAngle);
        xs.WriteFloat(xe, nameof(_forceFeedbackMultiplier), _forceFeedbackMultiplier);
        xs.WriteFloat(xe, nameof(_maxContactBodyAcceleration), _maxContactBodyAcceleration);
        xs.WriteNumber(xe, nameof(_axle), _axle);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleDataWheelComponentParams other && _radius == other._radius && _mass == other._mass && _width == other._width && _friction == other._friction && _viscosityFriction == other._viscosityFriction && _maxFriction == other._maxFriction && _slipAngle == other._slipAngle && _forceFeedbackMultiplier == other._forceFeedbackMultiplier && _maxContactBodyAcceleration == other._maxContactBodyAcceleration && _axle == other._axle && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleDataWheelComponentParams? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleDataWheelComponentParams? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_radius);
        code.Add(_mass);
        code.Add(_width);
        code.Add(_friction);
        code.Add(_viscosityFriction);
        code.Add(_maxFriction);
        code.Add(_slipAngle);
        code.Add(_forceFeedbackMultiplier);
        code.Add(_maxContactBodyAcceleration);
        code.Add(_axle);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
