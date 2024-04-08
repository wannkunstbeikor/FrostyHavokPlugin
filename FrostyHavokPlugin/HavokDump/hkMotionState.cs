using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMotionState : IHavokObject, IEquatable<hkMotionState?>
{
    public virtual uint Signature => 0;
    public Matrix4 _transform;
    public Vector4[] _sweptTransform = new Vector4[5];
    public Vector4 _deltaAngle;
    public float _objectRadius;
    public Half _linearDamping;
    public Half _angularDamping;
    public Half _timeFactor;
    public hkUFloat8 _maxLinearVelocity;
    public hkUFloat8 _maxAngularVelocity;
    public byte _deactivationClass;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transform = des.ReadTransform(br);
        _sweptTransform = des.ReadVector4CStyleArray(br, 5);
        _deltaAngle = des.ReadVector4(br);
        _objectRadius = br.ReadSingle();
        _linearDamping = des.ReadHalf(br);
        _angularDamping = des.ReadHalf(br);
        _timeFactor = des.ReadHalf(br);
        _maxLinearVelocity = new hkUFloat8();
        _maxLinearVelocity.Read(des, br);
        _maxAngularVelocity = new hkUFloat8();
        _maxAngularVelocity.Read(des, br);
        _deactivationClass = br.ReadByte();
        br.Position += 3; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteTransform(bw, _transform);
        s.WriteVector4CStyleArray(bw, _sweptTransform);
        s.WriteVector4(bw, _deltaAngle);
        bw.WriteSingle(_objectRadius);
        s.WriteHalf(bw, _linearDamping);
        s.WriteHalf(bw, _angularDamping);
        s.WriteHalf(bw, _timeFactor);
        _maxLinearVelocity.Write(s, bw);
        _maxAngularVelocity.Write(s, bw);
        bw.WriteByte(_deactivationClass);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteTransform(xe, nameof(_transform), _transform);
        xs.WriteVector4Array(xe, nameof(_sweptTransform), _sweptTransform);
        xs.WriteVector4(xe, nameof(_deltaAngle), _deltaAngle);
        xs.WriteFloat(xe, nameof(_objectRadius), _objectRadius);
        xs.WriteFloat(xe, nameof(_linearDamping), _linearDamping);
        xs.WriteFloat(xe, nameof(_angularDamping), _angularDamping);
        xs.WriteFloat(xe, nameof(_timeFactor), _timeFactor);
        xs.WriteClass(xe, nameof(_maxLinearVelocity), _maxLinearVelocity);
        xs.WriteClass(xe, nameof(_maxAngularVelocity), _maxAngularVelocity);
        xs.WriteNumber(xe, nameof(_deactivationClass), _deactivationClass);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMotionState);
    }
    public bool Equals(hkMotionState? other)
    {
        return other is not null && _transform.Equals(other._transform) && _sweptTransform.Equals(other._sweptTransform) && _deltaAngle.Equals(other._deltaAngle) && _objectRadius.Equals(other._objectRadius) && _linearDamping.Equals(other._linearDamping) && _angularDamping.Equals(other._angularDamping) && _timeFactor.Equals(other._timeFactor) && _maxLinearVelocity.Equals(other._maxLinearVelocity) && _maxAngularVelocity.Equals(other._maxAngularVelocity) && _deactivationClass.Equals(other._deactivationClass) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transform);
        code.Add(_sweptTransform);
        code.Add(_deltaAngle);
        code.Add(_objectRadius);
        code.Add(_linearDamping);
        code.Add(_angularDamping);
        code.Add(_timeFactor);
        code.Add(_maxLinearVelocity);
        code.Add(_maxAngularVelocity);
        code.Add(_deactivationClass);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
