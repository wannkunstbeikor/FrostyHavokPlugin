using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleLinearCastWheelCollideWheelState : IHavokObject
{
    public virtual uint Signature => 0;
    public hkAabb? _aabb;
    public hknpShape? _shape;
    public Matrix4 _transform;
    public Vector4 _to;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _aabb = new hkAabb();
        _aabb.Read(des, br);
        _shape = des.ReadClassPointer<hknpShape>(br);
        br.Position += 8; // padding
        _transform = des.ReadTransform(br);
        _to = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _aabb.Write(s, bw);
        s.WriteClassPointer<hknpShape>(bw, _shape);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteTransform(bw, _transform);
        s.WriteVector4(bw, _to);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteTransform(xe, nameof(_transform), _transform);
        xs.WriteVector4(xe, nameof(_to), _to);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleLinearCastWheelCollideWheelState other && _aabb == other._aabb && _shape == other._shape && _transform == other._transform && _to == other._to && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleLinearCastWheelCollideWheelState? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleLinearCastWheelCollideWheelState? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(_shape);
        code.Add(_transform);
        code.Add(_to);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
