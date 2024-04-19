using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpLinearSurfaceVelocity : hknpSurfaceVelocity
{
    public override uint Signature => 0;
    public hknpSurfaceVelocity_Space _space;
    public hknpLinearSurfaceVelocity_ProjectMethod _projectMethod;
    public float _maxVelocityScale;
    public Vector4 _velocityMeasurePlane;
    public Vector4 _velocity;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _space = (hknpSurfaceVelocity_Space)br.ReadByte();
        _projectMethod = (hknpLinearSurfaceVelocity_ProjectMethod)br.ReadByte();
        br.Position += 2; // padding
        _maxVelocityScale = br.ReadSingle();
        br.Position += 8; // padding
        _velocityMeasurePlane = des.ReadVector4(br);
        _velocity = des.ReadVector4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte((byte)_space);
        bw.WriteByte((byte)_projectMethod);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_maxVelocityScale);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _velocityMeasurePlane);
        s.WriteVector4(bw, _velocity);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hknpSurfaceVelocity_Space, byte>(xe, nameof(_space), (byte)_space);
        xs.WriteEnum<hknpLinearSurfaceVelocity_ProjectMethod, byte>(xe, nameof(_projectMethod), (byte)_projectMethod);
        xs.WriteFloat(xe, nameof(_maxVelocityScale), _maxVelocityScale);
        xs.WriteVector4(xe, nameof(_velocityMeasurePlane), _velocityMeasurePlane);
        xs.WriteVector4(xe, nameof(_velocity), _velocity);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpLinearSurfaceVelocity other && base.Equals(other) && _space == other._space && _projectMethod == other._projectMethod && _maxVelocityScale == other._maxVelocityScale && _velocityMeasurePlane == other._velocityMeasurePlane && _velocity == other._velocity && Signature == other.Signature;
    }
    public static bool operator ==(hknpLinearSurfaceVelocity? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpLinearSurfaceVelocity? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_space);
        code.Add(_projectMethod);
        code.Add(_maxVelocityScale);
        code.Add(_velocityMeasurePlane);
        code.Add(_velocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
