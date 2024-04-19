using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWheelFrictionConstraintAtomAxle : IHavokObject
{
    public virtual uint Signature => 0;
    public float _spinVelocity;
    public float _sumVelocity;
    public int _numWheels;
    public int _wheelsSolved;
    public int _stepsSolved;
    public float _invInertia;
    public float _inertia;
    public float _impulseScaling;
    public float _impulseMax;
    public bool _isFixed;
    public int _numWheelsOnGround;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _spinVelocity = br.ReadSingle();
        _sumVelocity = br.ReadSingle();
        _numWheels = br.ReadInt32();
        _wheelsSolved = br.ReadInt32();
        _stepsSolved = br.ReadInt32();
        _invInertia = br.ReadSingle();
        _inertia = br.ReadSingle();
        _impulseScaling = br.ReadSingle();
        _impulseMax = br.ReadSingle();
        _isFixed = br.ReadBoolean();
        br.Position += 3; // padding
        _numWheelsOnGround = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_spinVelocity);
        bw.WriteSingle(_sumVelocity);
        bw.WriteInt32(_numWheels);
        bw.WriteInt32(_wheelsSolved);
        bw.WriteInt32(_stepsSolved);
        bw.WriteSingle(_invInertia);
        bw.WriteSingle(_inertia);
        bw.WriteSingle(_impulseScaling);
        bw.WriteSingle(_impulseMax);
        bw.WriteBoolean(_isFixed);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numWheelsOnGround);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_spinVelocity), _spinVelocity);
        xs.WriteFloat(xe, nameof(_sumVelocity), _sumVelocity);
        xs.WriteNumber(xe, nameof(_numWheels), _numWheels);
        xs.WriteNumber(xe, nameof(_wheelsSolved), _wheelsSolved);
        xs.WriteNumber(xe, nameof(_stepsSolved), _stepsSolved);
        xs.WriteFloat(xe, nameof(_invInertia), _invInertia);
        xs.WriteFloat(xe, nameof(_inertia), _inertia);
        xs.WriteFloat(xe, nameof(_impulseScaling), _impulseScaling);
        xs.WriteFloat(xe, nameof(_impulseMax), _impulseMax);
        xs.WriteBoolean(xe, nameof(_isFixed), _isFixed);
        xs.WriteNumber(xe, nameof(_numWheelsOnGround), _numWheelsOnGround);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpWheelFrictionConstraintAtomAxle other && _spinVelocity == other._spinVelocity && _sumVelocity == other._sumVelocity && _numWheels == other._numWheels && _wheelsSolved == other._wheelsSolved && _stepsSolved == other._stepsSolved && _invInertia == other._invInertia && _inertia == other._inertia && _impulseScaling == other._impulseScaling && _impulseMax == other._impulseMax && _isFixed == other._isFixed && _numWheelsOnGround == other._numWheelsOnGround && Signature == other.Signature;
    }
    public static bool operator ==(hkpWheelFrictionConstraintAtomAxle? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpWheelFrictionConstraintAtomAxle? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_spinVelocity);
        code.Add(_sumVelocity);
        code.Add(_numWheels);
        code.Add(_wheelsSolved);
        code.Add(_stepsSolved);
        code.Add(_invInertia);
        code.Add(_inertia);
        code.Add(_impulseScaling);
        code.Add(_impulseMax);
        code.Add(_isFixed);
        code.Add(_numWheelsOnGround);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
