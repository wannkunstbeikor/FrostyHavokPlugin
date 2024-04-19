using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleSuspensionSuspensionWheelParameters : IHavokObject
{
    public virtual uint Signature => 0;
    public Vector4 _hardpointChassisSpace;
    public Vector4 _directionChassisSpace;
    public float _length;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _hardpointChassisSpace = des.ReadVector4(br);
        _directionChassisSpace = des.ReadVector4(br);
        _length = br.ReadSingle();
        br.Position += 12; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _hardpointChassisSpace);
        s.WriteVector4(bw, _directionChassisSpace);
        bw.WriteSingle(_length);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_hardpointChassisSpace), _hardpointChassisSpace);
        xs.WriteVector4(xe, nameof(_directionChassisSpace), _directionChassisSpace);
        xs.WriteFloat(xe, nameof(_length), _length);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleSuspensionSuspensionWheelParameters other && _hardpointChassisSpace == other._hardpointChassisSpace && _directionChassisSpace == other._directionChassisSpace && _length == other._length && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleSuspensionSuspensionWheelParameters? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleSuspensionSuspensionWheelParameters? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_hardpointChassisSpace);
        code.Add(_directionChassisSpace);
        code.Add(_length);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
