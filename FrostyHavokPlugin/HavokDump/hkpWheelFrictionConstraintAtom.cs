using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWheelFrictionConstraintAtom : hkpConstraintAtom, IEquatable<hkpWheelFrictionConstraintAtom?>
{
    public override uint Signature => 0;
    public byte _isEnabled;
    public byte _forwardAxis;
    public byte _sideAxis;
    public float _maxFrictionForce;
    public float _torque;
    public float _radius;
    public float[] _frictionImpulse = new float[2];
    public float[] _slipImpulse = new float[2];
    public hkpWheelFrictionConstraintAtomAxle _axle;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadByte();
        _forwardAxis = br.ReadByte();
        _sideAxis = br.ReadByte();
        br.Position += 3; // padding
        _maxFrictionForce = br.ReadSingle();
        _torque = br.ReadSingle();
        _radius = br.ReadSingle();
        _frictionImpulse = des.ReadSingleCStyleArray(br, 2);
        _slipImpulse = des.ReadSingleCStyleArray(br, 2);
        br.Position += 4; // padding
        _axle = des.ReadClassPointer<hkpWheelFrictionConstraintAtomAxle>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_isEnabled);
        bw.WriteByte(_forwardAxis);
        bw.WriteByte(_sideAxis);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_maxFrictionForce);
        bw.WriteSingle(_torque);
        bw.WriteSingle(_radius);
        s.WriteSingleCStyleArray(bw, _frictionImpulse);
        s.WriteSingleCStyleArray(bw, _slipImpulse);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hkpWheelFrictionConstraintAtomAxle>(bw, _axle);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_forwardAxis), _forwardAxis);
        xs.WriteNumber(xe, nameof(_sideAxis), _sideAxis);
        xs.WriteFloat(xe, nameof(_maxFrictionForce), _maxFrictionForce);
        xs.WriteFloat(xe, nameof(_torque), _torque);
        xs.WriteFloat(xe, nameof(_radius), _radius);
        xs.WriteFloatArray(xe, nameof(_frictionImpulse), _frictionImpulse);
        xs.WriteFloatArray(xe, nameof(_slipImpulse), _slipImpulse);
        xs.WriteClassPointer(xe, nameof(_axle), _axle);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpWheelFrictionConstraintAtom);
    }
    public bool Equals(hkpWheelFrictionConstraintAtom? other)
    {
        return other is not null && _isEnabled.Equals(other._isEnabled) && _forwardAxis.Equals(other._forwardAxis) && _sideAxis.Equals(other._sideAxis) && _maxFrictionForce.Equals(other._maxFrictionForce) && _torque.Equals(other._torque) && _radius.Equals(other._radius) && _frictionImpulse.Equals(other._frictionImpulse) && _slipImpulse.Equals(other._slipImpulse) && _axle.Equals(other._axle) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_forwardAxis);
        code.Add(_sideAxis);
        code.Add(_maxFrictionForce);
        code.Add(_torque);
        code.Add(_radius);
        code.Add(_frictionImpulse);
        code.Add(_slipImpulse);
        code.Add(_axle);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
