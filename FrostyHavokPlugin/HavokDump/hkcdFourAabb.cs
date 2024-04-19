using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdFourAabb : IHavokObject
{
    public virtual uint Signature => 0;
    public Vector4 _lx;
    public Vector4 _hx;
    public Vector4 _ly;
    public Vector4 _hy;
    public Vector4 _lz;
    public Vector4 _hz;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _lx = des.ReadVector4(br);
        _hx = des.ReadVector4(br);
        _ly = des.ReadVector4(br);
        _hy = des.ReadVector4(br);
        _lz = des.ReadVector4(br);
        _hz = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _lx);
        s.WriteVector4(bw, _hx);
        s.WriteVector4(bw, _ly);
        s.WriteVector4(bw, _hy);
        s.WriteVector4(bw, _lz);
        s.WriteVector4(bw, _hz);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_lx), _lx);
        xs.WriteVector4(xe, nameof(_hx), _hx);
        xs.WriteVector4(xe, nameof(_ly), _ly);
        xs.WriteVector4(xe, nameof(_hy), _hy);
        xs.WriteVector4(xe, nameof(_lz), _lz);
        xs.WriteVector4(xe, nameof(_hz), _hz);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdFourAabb other && _lx == other._lx && _hx == other._hx && _ly == other._ly && _hy == other._hy && _lz == other._lz && _hz == other._hz && Signature == other.Signature;
    }
    public static bool operator ==(hkcdFourAabb? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdFourAabb? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_lx);
        code.Add(_hx);
        code.Add(_ly);
        code.Add(_hy);
        code.Add(_lz);
        code.Add(_hz);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
