using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultAerodynamics : hknpVehicleAerodynamics, IEquatable<hknpVehicleDefaultAerodynamics?>
{
    public override uint Signature => 0;
    public float _airDensity;
    public float _frontalArea;
    public float _dragCoefficient;
    public float _liftCoefficient;
    public Vector4 _extraGravityws;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _airDensity = br.ReadSingle();
        _frontalArea = br.ReadSingle();
        _dragCoefficient = br.ReadSingle();
        _liftCoefficient = br.ReadSingle();
        _extraGravityws = des.ReadVector4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_airDensity);
        bw.WriteSingle(_frontalArea);
        bw.WriteSingle(_dragCoefficient);
        bw.WriteSingle(_liftCoefficient);
        s.WriteVector4(bw, _extraGravityws);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_airDensity), _airDensity);
        xs.WriteFloat(xe, nameof(_frontalArea), _frontalArea);
        xs.WriteFloat(xe, nameof(_dragCoefficient), _dragCoefficient);
        xs.WriteFloat(xe, nameof(_liftCoefficient), _liftCoefficient);
        xs.WriteVector4(xe, nameof(_extraGravityws), _extraGravityws);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultAerodynamics);
    }
    public bool Equals(hknpVehicleDefaultAerodynamics? other)
    {
        return other is not null && _airDensity.Equals(other._airDensity) && _frontalArea.Equals(other._frontalArea) && _dragCoefficient.Equals(other._dragCoefficient) && _liftCoefficient.Equals(other._liftCoefficient) && _extraGravityws.Equals(other._extraGravityws) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_airDensity);
        code.Add(_frontalArea);
        code.Add(_dragCoefficient);
        code.Add(_liftCoefficient);
        code.Add(_extraGravityws);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
