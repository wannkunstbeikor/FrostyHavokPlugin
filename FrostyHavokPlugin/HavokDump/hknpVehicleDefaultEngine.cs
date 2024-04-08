using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultEngine : hknpVehicleEngine, IEquatable<hknpVehicleDefaultEngine?>
{
    public override uint Signature => 0;
    public float _minRPM;
    public float _optRPM;
    public float _maxRPM;
    public float _maxTorque;
    public float _torqueFactorAtMinRPM;
    public float _torqueFactorAtMaxRPM;
    public float _resistanceFactorAtMinRPM;
    public float _resistanceFactorAtOptRPM;
    public float _resistanceFactorAtMaxRPM;
    public float _clutchSlipRPM;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _minRPM = br.ReadSingle();
        _optRPM = br.ReadSingle();
        _maxRPM = br.ReadSingle();
        _maxTorque = br.ReadSingle();
        _torqueFactorAtMinRPM = br.ReadSingle();
        _torqueFactorAtMaxRPM = br.ReadSingle();
        _resistanceFactorAtMinRPM = br.ReadSingle();
        _resistanceFactorAtOptRPM = br.ReadSingle();
        _resistanceFactorAtMaxRPM = br.ReadSingle();
        _clutchSlipRPM = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_minRPM);
        bw.WriteSingle(_optRPM);
        bw.WriteSingle(_maxRPM);
        bw.WriteSingle(_maxTorque);
        bw.WriteSingle(_torqueFactorAtMinRPM);
        bw.WriteSingle(_torqueFactorAtMaxRPM);
        bw.WriteSingle(_resistanceFactorAtMinRPM);
        bw.WriteSingle(_resistanceFactorAtOptRPM);
        bw.WriteSingle(_resistanceFactorAtMaxRPM);
        bw.WriteSingle(_clutchSlipRPM);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_minRPM), _minRPM);
        xs.WriteFloat(xe, nameof(_optRPM), _optRPM);
        xs.WriteFloat(xe, nameof(_maxRPM), _maxRPM);
        xs.WriteFloat(xe, nameof(_maxTorque), _maxTorque);
        xs.WriteFloat(xe, nameof(_torqueFactorAtMinRPM), _torqueFactorAtMinRPM);
        xs.WriteFloat(xe, nameof(_torqueFactorAtMaxRPM), _torqueFactorAtMaxRPM);
        xs.WriteFloat(xe, nameof(_resistanceFactorAtMinRPM), _resistanceFactorAtMinRPM);
        xs.WriteFloat(xe, nameof(_resistanceFactorAtOptRPM), _resistanceFactorAtOptRPM);
        xs.WriteFloat(xe, nameof(_resistanceFactorAtMaxRPM), _resistanceFactorAtMaxRPM);
        xs.WriteFloat(xe, nameof(_clutchSlipRPM), _clutchSlipRPM);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultEngine);
    }
    public bool Equals(hknpVehicleDefaultEngine? other)
    {
        return other is not null && _minRPM.Equals(other._minRPM) && _optRPM.Equals(other._optRPM) && _maxRPM.Equals(other._maxRPM) && _maxTorque.Equals(other._maxTorque) && _torqueFactorAtMinRPM.Equals(other._torqueFactorAtMinRPM) && _torqueFactorAtMaxRPM.Equals(other._torqueFactorAtMaxRPM) && _resistanceFactorAtMinRPM.Equals(other._resistanceFactorAtMinRPM) && _resistanceFactorAtOptRPM.Equals(other._resistanceFactorAtOptRPM) && _resistanceFactorAtMaxRPM.Equals(other._resistanceFactorAtMaxRPM) && _clutchSlipRPM.Equals(other._clutchSlipRPM) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_minRPM);
        code.Add(_optRPM);
        code.Add(_maxRPM);
        code.Add(_maxTorque);
        code.Add(_torqueFactorAtMinRPM);
        code.Add(_torqueFactorAtMaxRPM);
        code.Add(_resistanceFactorAtMinRPM);
        code.Add(_resistanceFactorAtOptRPM);
        code.Add(_resistanceFactorAtMaxRPM);
        code.Add(_clutchSlipRPM);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
