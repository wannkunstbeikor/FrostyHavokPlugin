using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdFourAabb : IHavokObject, IEquatable<hkcdFourAabb?>
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
        return Equals(obj as hkcdFourAabb);
    }
    public bool Equals(hkcdFourAabb? other)
    {
        return other is not null && _lx.Equals(other._lx) && _hx.Equals(other._hx) && _ly.Equals(other._ly) && _hy.Equals(other._hy) && _lz.Equals(other._lz) && _hz.Equals(other._hz) && Signature == other.Signature;
    }
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