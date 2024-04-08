using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleFrictionDescriptionAxisDescription : IHavokObject, IEquatable<hknpVehicleFrictionDescriptionAxisDescription?>
{
    public virtual uint Signature => 0;
    public float[] _frictionCircleYtab = new float[16];
    public float _xStep;
    public float _xStart;
    public float _wheelSurfaceInertia;
    public float _wheelSurfaceInertiaInv;
    public float _wheelChassisMassRatio;
    public float _wheelRadius;
    public float _wheelRadiusInv;
    public float _wheelDownForceFactor;
    public float _wheelDownForceSumFactor;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _frictionCircleYtab = des.ReadSingleCStyleArray(br, 16);
        _xStep = br.ReadSingle();
        _xStart = br.ReadSingle();
        _wheelSurfaceInertia = br.ReadSingle();
        _wheelSurfaceInertiaInv = br.ReadSingle();
        _wheelChassisMassRatio = br.ReadSingle();
        _wheelRadius = br.ReadSingle();
        _wheelRadiusInv = br.ReadSingle();
        _wheelDownForceFactor = br.ReadSingle();
        _wheelDownForceSumFactor = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteSingleCStyleArray(bw, _frictionCircleYtab);
        bw.WriteSingle(_xStep);
        bw.WriteSingle(_xStart);
        bw.WriteSingle(_wheelSurfaceInertia);
        bw.WriteSingle(_wheelSurfaceInertiaInv);
        bw.WriteSingle(_wheelChassisMassRatio);
        bw.WriteSingle(_wheelRadius);
        bw.WriteSingle(_wheelRadiusInv);
        bw.WriteSingle(_wheelDownForceFactor);
        bw.WriteSingle(_wheelDownForceSumFactor);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloatArray(xe, nameof(_frictionCircleYtab), _frictionCircleYtab);
        xs.WriteFloat(xe, nameof(_xStep), _xStep);
        xs.WriteFloat(xe, nameof(_xStart), _xStart);
        xs.WriteFloat(xe, nameof(_wheelSurfaceInertia), _wheelSurfaceInertia);
        xs.WriteFloat(xe, nameof(_wheelSurfaceInertiaInv), _wheelSurfaceInertiaInv);
        xs.WriteFloat(xe, nameof(_wheelChassisMassRatio), _wheelChassisMassRatio);
        xs.WriteFloat(xe, nameof(_wheelRadius), _wheelRadius);
        xs.WriteFloat(xe, nameof(_wheelRadiusInv), _wheelRadiusInv);
        xs.WriteFloat(xe, nameof(_wheelDownForceFactor), _wheelDownForceFactor);
        xs.WriteFloat(xe, nameof(_wheelDownForceSumFactor), _wheelDownForceSumFactor);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleFrictionDescriptionAxisDescription);
    }
    public bool Equals(hknpVehicleFrictionDescriptionAxisDescription? other)
    {
        return other is not null && _frictionCircleYtab.Equals(other._frictionCircleYtab) && _xStep.Equals(other._xStep) && _xStart.Equals(other._xStart) && _wheelSurfaceInertia.Equals(other._wheelSurfaceInertia) && _wheelSurfaceInertiaInv.Equals(other._wheelSurfaceInertiaInv) && _wheelChassisMassRatio.Equals(other._wheelChassisMassRatio) && _wheelRadius.Equals(other._wheelRadius) && _wheelRadiusInv.Equals(other._wheelRadiusInv) && _wheelDownForceFactor.Equals(other._wheelDownForceFactor) && _wheelDownForceSumFactor.Equals(other._wheelDownForceSumFactor) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_frictionCircleYtab);
        code.Add(_xStep);
        code.Add(_xStart);
        code.Add(_wheelSurfaceInertia);
        code.Add(_wheelSurfaceInertiaInv);
        code.Add(_wheelChassisMassRatio);
        code.Add(_wheelRadius);
        code.Add(_wheelRadiusInv);
        code.Add(_wheelDownForceFactor);
        code.Add(_wheelDownForceSumFactor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
