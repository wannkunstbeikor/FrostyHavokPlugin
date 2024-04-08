using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultAnalogDriverInput : hknpVehicleDriverInput, IEquatable<hknpVehicleDefaultAnalogDriverInput?>
{
    public override uint Signature => 0;
    public float _slopeChangePointX;
    public float _initialSlope;
    public float _deadZone;
    public bool _autoReverse;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _slopeChangePointX = br.ReadSingle();
        _initialSlope = br.ReadSingle();
        _deadZone = br.ReadSingle();
        _autoReverse = br.ReadBoolean();
        br.Position += 3; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_slopeChangePointX);
        bw.WriteSingle(_initialSlope);
        bw.WriteSingle(_deadZone);
        bw.WriteBoolean(_autoReverse);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_slopeChangePointX), _slopeChangePointX);
        xs.WriteFloat(xe, nameof(_initialSlope), _initialSlope);
        xs.WriteFloat(xe, nameof(_deadZone), _deadZone);
        xs.WriteBoolean(xe, nameof(_autoReverse), _autoReverse);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultAnalogDriverInput);
    }
    public bool Equals(hknpVehicleDefaultAnalogDriverInput? other)
    {
        return other is not null && _slopeChangePointX.Equals(other._slopeChangePointX) && _initialSlope.Equals(other._initialSlope) && _deadZone.Equals(other._deadZone) && _autoReverse.Equals(other._autoReverse) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_slopeChangePointX);
        code.Add(_initialSlope);
        code.Add(_deadZone);
        code.Add(_autoReverse);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
