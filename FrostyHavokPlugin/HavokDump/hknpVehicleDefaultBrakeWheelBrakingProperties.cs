using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultBrakeWheelBrakingProperties : IHavokObject
{
    public virtual uint Signature => 0;
    public float _maxBreakingTorque;
    public float _minPedalInputToBlock;
    public bool _isConnectedToHandbrake;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _maxBreakingTorque = br.ReadSingle();
        _minPedalInputToBlock = br.ReadSingle();
        _isConnectedToHandbrake = br.ReadBoolean();
        br.Position += 3; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_maxBreakingTorque);
        bw.WriteSingle(_minPedalInputToBlock);
        bw.WriteBoolean(_isConnectedToHandbrake);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_maxBreakingTorque), _maxBreakingTorque);
        xs.WriteFloat(xe, nameof(_minPedalInputToBlock), _minPedalInputToBlock);
        xs.WriteBoolean(xe, nameof(_isConnectedToHandbrake), _isConnectedToHandbrake);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleDefaultBrakeWheelBrakingProperties other && _maxBreakingTorque == other._maxBreakingTorque && _minPedalInputToBlock == other._minPedalInputToBlock && _isConnectedToHandbrake == other._isConnectedToHandbrake && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleDefaultBrakeWheelBrakingProperties? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleDefaultBrakeWheelBrakingProperties? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_maxBreakingTorque);
        code.Add(_minPedalInputToBlock);
        code.Add(_isConnectedToHandbrake);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
