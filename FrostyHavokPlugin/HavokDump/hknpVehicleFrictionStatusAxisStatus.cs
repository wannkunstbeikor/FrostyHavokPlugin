using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleFrictionStatusAxisStatus : IHavokObject, IEquatable<hknpVehicleFrictionStatusAxisStatus?>
{
    public virtual uint Signature => 0;
    public float _forward_slip_velocity;
    public float _side_slip_velocity;
    public float _skid_energy_density;
    public float _side_force;
    public float _delayed_forward_impulse;
    public float _sideRhs;
    public float _forwardRhs;
    public float _relativeSideForce;
    public float _relativeForwardForce;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _forward_slip_velocity = br.ReadSingle();
        _side_slip_velocity = br.ReadSingle();
        _skid_energy_density = br.ReadSingle();
        _side_force = br.ReadSingle();
        _delayed_forward_impulse = br.ReadSingle();
        _sideRhs = br.ReadSingle();
        _forwardRhs = br.ReadSingle();
        _relativeSideForce = br.ReadSingle();
        _relativeForwardForce = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_forward_slip_velocity);
        bw.WriteSingle(_side_slip_velocity);
        bw.WriteSingle(_skid_energy_density);
        bw.WriteSingle(_side_force);
        bw.WriteSingle(_delayed_forward_impulse);
        bw.WriteSingle(_sideRhs);
        bw.WriteSingle(_forwardRhs);
        bw.WriteSingle(_relativeSideForce);
        bw.WriteSingle(_relativeForwardForce);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_forward_slip_velocity), _forward_slip_velocity);
        xs.WriteFloat(xe, nameof(_side_slip_velocity), _side_slip_velocity);
        xs.WriteFloat(xe, nameof(_skid_energy_density), _skid_energy_density);
        xs.WriteFloat(xe, nameof(_side_force), _side_force);
        xs.WriteFloat(xe, nameof(_delayed_forward_impulse), _delayed_forward_impulse);
        xs.WriteFloat(xe, nameof(_sideRhs), _sideRhs);
        xs.WriteFloat(xe, nameof(_forwardRhs), _forwardRhs);
        xs.WriteFloat(xe, nameof(_relativeSideForce), _relativeSideForce);
        xs.WriteFloat(xe, nameof(_relativeForwardForce), _relativeForwardForce);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleFrictionStatusAxisStatus);
    }
    public bool Equals(hknpVehicleFrictionStatusAxisStatus? other)
    {
        return other is not null && _forward_slip_velocity.Equals(other._forward_slip_velocity) && _side_slip_velocity.Equals(other._side_slip_velocity) && _skid_energy_density.Equals(other._skid_energy_density) && _side_force.Equals(other._side_force) && _delayed_forward_impulse.Equals(other._delayed_forward_impulse) && _sideRhs.Equals(other._sideRhs) && _forwardRhs.Equals(other._forwardRhs) && _relativeSideForce.Equals(other._relativeSideForce) && _relativeForwardForce.Equals(other._relativeForwardForce) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_forward_slip_velocity);
        code.Add(_side_slip_velocity);
        code.Add(_skid_energy_density);
        code.Add(_side_force);
        code.Add(_delayed_forward_impulse);
        code.Add(_sideRhs);
        code.Add(_forwardRhs);
        code.Add(_relativeSideForce);
        code.Add(_relativeForwardForce);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
