using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSweptTransformf : IHavokObject, IEquatable<hkSweptTransformf?>
{
    public virtual uint Signature => 0;
    public Vector4 _centerOfMass0;
    public Vector4 _centerOfMass1;
    public Quaternion _rotation0;
    public Quaternion _rotation1;
    public Vector4 _centerOfMassLocal;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _centerOfMass0 = des.ReadVector4(br);
        _centerOfMass1 = des.ReadVector4(br);
        _rotation0 = des.ReadQuaternion(br);
        _rotation1 = des.ReadQuaternion(br);
        _centerOfMassLocal = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _centerOfMass0);
        s.WriteVector4(bw, _centerOfMass1);
        s.WriteQuaternion(bw, _rotation0);
        s.WriteQuaternion(bw, _rotation1);
        s.WriteVector4(bw, _centerOfMassLocal);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_centerOfMass0), _centerOfMass0);
        xs.WriteVector4(xe, nameof(_centerOfMass1), _centerOfMass1);
        xs.WriteQuaternion(xe, nameof(_rotation0), _rotation0);
        xs.WriteQuaternion(xe, nameof(_rotation1), _rotation1);
        xs.WriteVector4(xe, nameof(_centerOfMassLocal), _centerOfMassLocal);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSweptTransformf);
    }
    public bool Equals(hkSweptTransformf? other)
    {
        return other is not null && _centerOfMass0.Equals(other._centerOfMass0) && _centerOfMass1.Equals(other._centerOfMass1) && _rotation0.Equals(other._rotation0) && _rotation1.Equals(other._rotation1) && _centerOfMassLocal.Equals(other._centerOfMassLocal) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_centerOfMass0);
        code.Add(_centerOfMass1);
        code.Add(_rotation0);
        code.Add(_rotation1);
        code.Add(_centerOfMassLocal);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
