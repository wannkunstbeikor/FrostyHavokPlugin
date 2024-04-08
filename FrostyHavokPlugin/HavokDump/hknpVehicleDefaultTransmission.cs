using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultTransmission : hknpVehicleTransmission, IEquatable<hknpVehicleDefaultTransmission?>
{
    public override uint Signature => 0;
    public float _downshiftRPM;
    public float _upshiftRPM;
    public float _primaryTransmissionRatio;
    public float _clutchDelayTime;
    public float _reverseGearRatio;
    public List<float> _gearsRatio;
    public List<float> _wheelsTorqueRatio;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _downshiftRPM = br.ReadSingle();
        _upshiftRPM = br.ReadSingle();
        _primaryTransmissionRatio = br.ReadSingle();
        _clutchDelayTime = br.ReadSingle();
        _reverseGearRatio = br.ReadSingle();
        br.Position += 4; // padding
        _gearsRatio = des.ReadSingleArray(br);
        _wheelsTorqueRatio = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_downshiftRPM);
        bw.WriteSingle(_upshiftRPM);
        bw.WriteSingle(_primaryTransmissionRatio);
        bw.WriteSingle(_clutchDelayTime);
        bw.WriteSingle(_reverseGearRatio);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteSingleArray(bw, _gearsRatio);
        s.WriteSingleArray(bw, _wheelsTorqueRatio);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_downshiftRPM), _downshiftRPM);
        xs.WriteFloat(xe, nameof(_upshiftRPM), _upshiftRPM);
        xs.WriteFloat(xe, nameof(_primaryTransmissionRatio), _primaryTransmissionRatio);
        xs.WriteFloat(xe, nameof(_clutchDelayTime), _clutchDelayTime);
        xs.WriteFloat(xe, nameof(_reverseGearRatio), _reverseGearRatio);
        xs.WriteFloatArray(xe, nameof(_gearsRatio), _gearsRatio);
        xs.WriteFloatArray(xe, nameof(_wheelsTorqueRatio), _wheelsTorqueRatio);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultTransmission);
    }
    public bool Equals(hknpVehicleDefaultTransmission? other)
    {
        return other is not null && _downshiftRPM.Equals(other._downshiftRPM) && _upshiftRPM.Equals(other._upshiftRPM) && _primaryTransmissionRatio.Equals(other._primaryTransmissionRatio) && _clutchDelayTime.Equals(other._clutchDelayTime) && _reverseGearRatio.Equals(other._reverseGearRatio) && _gearsRatio.Equals(other._gearsRatio) && _wheelsTorqueRatio.Equals(other._wheelsTorqueRatio) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_downshiftRPM);
        code.Add(_upshiftRPM);
        code.Add(_primaryTransmissionRatio);
        code.Add(_clutchDelayTime);
        code.Add(_reverseGearRatio);
        code.Add(_gearsRatio);
        code.Add(_wheelsTorqueRatio);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
