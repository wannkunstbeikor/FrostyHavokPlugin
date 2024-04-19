using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleRayCastWheelCollide : hknpVehicleWheelCollide
{
    public override uint Signature => 0;
    public uint _wheelCollisionFilterInfo;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelCollisionFilterInfo = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_wheelCollisionFilterInfo);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_wheelCollisionFilterInfo), _wheelCollisionFilterInfo);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleRayCastWheelCollide other && base.Equals(other) && _wheelCollisionFilterInfo == other._wheelCollisionFilterInfo && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleRayCastWheelCollide? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleRayCastWheelCollide? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelCollisionFilterInfo);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
