using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleLinearCastWheelCollide : hknpVehicleWheelCollide
{
    public override uint Signature => 0;
    public List<hknpVehicleLinearCastWheelCollideWheelState?> _wheelStates = new();
    public float _maxExtraPenetration;
    public float _startPointTolerance;
    public uint _chassisBody;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelStates = des.ReadClassArray<hknpVehicleLinearCastWheelCollideWheelState>(br);
        _maxExtraPenetration = br.ReadSingle();
        _startPointTolerance = br.ReadSingle();
        _chassisBody = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hknpVehicleLinearCastWheelCollideWheelState>(bw, _wheelStates);
        bw.WriteSingle(_maxExtraPenetration);
        bw.WriteSingle(_startPointTolerance);
        bw.WriteUInt32(_chassisBody);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hknpVehicleLinearCastWheelCollideWheelState>(xe, nameof(_wheelStates), _wheelStates);
        xs.WriteFloat(xe, nameof(_maxExtraPenetration), _maxExtraPenetration);
        xs.WriteFloat(xe, nameof(_startPointTolerance), _startPointTolerance);
        xs.WriteNumber(xe, nameof(_chassisBody), _chassisBody);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleLinearCastWheelCollide other && base.Equals(other) && _wheelStates.SequenceEqual(other._wheelStates) && _maxExtraPenetration == other._maxExtraPenetration && _startPointTolerance == other._startPointTolerance && _chassisBody == other._chassisBody && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleLinearCastWheelCollide? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleLinearCastWheelCollide? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelStates);
        code.Add(_maxExtraPenetration);
        code.Add(_startPointTolerance);
        code.Add(_chassisBody);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
