using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultSteering : hknpVehicleSteering
{
    public override uint Signature => 0;
    public float _maxSteeringAngle;
    public float _maxSpeedFullSteeringAngle;
    public List<bool> _doesWheelSteer = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _maxSteeringAngle = br.ReadSingle();
        _maxSpeedFullSteeringAngle = br.ReadSingle();
        _doesWheelSteer = des.ReadBooleanArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_maxSteeringAngle);
        bw.WriteSingle(_maxSpeedFullSteeringAngle);
        s.WriteBooleanArray(bw, _doesWheelSteer);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_maxSteeringAngle), _maxSteeringAngle);
        xs.WriteFloat(xe, nameof(_maxSpeedFullSteeringAngle), _maxSpeedFullSteeringAngle);
        xs.WriteBooleanArray(xe, nameof(_doesWheelSteer), _doesWheelSteer);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleDefaultSteering other && base.Equals(other) && _maxSteeringAngle == other._maxSteeringAngle && _maxSpeedFullSteeringAngle == other._maxSpeedFullSteeringAngle && _doesWheelSteer.SequenceEqual(other._doesWheelSteer) && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleDefaultSteering? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleDefaultSteering? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_maxSteeringAngle);
        code.Add(_maxSpeedFullSteeringAngle);
        code.Add(_doesWheelSteer);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
