using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxCamera : hkReferencedObject
{
    public override uint Signature => 0;
    public Vector4 _from;
    public Vector4 _focus;
    public Vector4 _up;
    public float _fov;
    public float _far;
    public float _near;
    public bool _leftHanded;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _from = des.ReadVector4(br);
        _focus = des.ReadVector4(br);
        _up = des.ReadVector4(br);
        _fov = br.ReadSingle();
        _far = br.ReadSingle();
        _near = br.ReadSingle();
        _leftHanded = br.ReadBoolean();
        br.Position += 3; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4(bw, _from);
        s.WriteVector4(bw, _focus);
        s.WriteVector4(bw, _up);
        bw.WriteSingle(_fov);
        bw.WriteSingle(_far);
        bw.WriteSingle(_near);
        bw.WriteBoolean(_leftHanded);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_from), _from);
        xs.WriteVector4(xe, nameof(_focus), _focus);
        xs.WriteVector4(xe, nameof(_up), _up);
        xs.WriteFloat(xe, nameof(_fov), _fov);
        xs.WriteFloat(xe, nameof(_far), _far);
        xs.WriteFloat(xe, nameof(_near), _near);
        xs.WriteBoolean(xe, nameof(_leftHanded), _leftHanded);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxCamera other && base.Equals(other) && _from == other._from && _focus == other._focus && _up == other._up && _fov == other._fov && _far == other._far && _near == other._near && _leftHanded == other._leftHanded && Signature == other.Signature;
    }
    public static bool operator ==(hkxCamera? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxCamera? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_from);
        code.Add(_focus);
        code.Add(_up);
        code.Add(_fov);
        code.Add(_far);
        code.Add(_near);
        code.Add(_leftHanded);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
