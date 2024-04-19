using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDriverInputAnalogStatus : hknpVehicleDriverInputStatus
{
    public override uint Signature => 0;
    public float _positionX;
    public float _positionY;
    public bool _handbrakeButtonPressed;
    public bool _reverseButtonPressed;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _positionX = br.ReadSingle();
        _positionY = br.ReadSingle();
        _handbrakeButtonPressed = br.ReadBoolean();
        _reverseButtonPressed = br.ReadBoolean();
        br.Position += 6; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_positionX);
        bw.WriteSingle(_positionY);
        bw.WriteBoolean(_handbrakeButtonPressed);
        bw.WriteBoolean(_reverseButtonPressed);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_positionX), _positionX);
        xs.WriteFloat(xe, nameof(_positionY), _positionY);
        xs.WriteBoolean(xe, nameof(_handbrakeButtonPressed), _handbrakeButtonPressed);
        xs.WriteBoolean(xe, nameof(_reverseButtonPressed), _reverseButtonPressed);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleDriverInputAnalogStatus other && base.Equals(other) && _positionX == other._positionX && _positionY == other._positionY && _handbrakeButtonPressed == other._handbrakeButtonPressed && _reverseButtonPressed == other._reverseButtonPressed && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleDriverInputAnalogStatus? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleDriverInputAnalogStatus? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_positionX);
        code.Add(_positionY);
        code.Add(_handbrakeButtonPressed);
        code.Add(_reverseButtonPressed);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
