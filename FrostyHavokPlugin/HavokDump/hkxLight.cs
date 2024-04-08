using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxLight : hkReferencedObject, IEquatable<hkxLight?>
{
    public override uint Signature => 0;
    public hkxLight_LightType _type;
    public Vector4 _position;
    public Vector4 _direction;
    public uint _color;
    public float _angle;
    public float _range;
    public float _fadeStart;
    public float _fadeEnd;
    public short _decayRate;
    public float _intensity;
    public bool _shadowCaster;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _type = (hkxLight_LightType)br.ReadSByte();
        br.Position += 15; // padding
        _position = des.ReadVector4(br);
        _direction = des.ReadVector4(br);
        _color = br.ReadUInt32();
        _angle = br.ReadSingle();
        _range = br.ReadSingle();
        _fadeStart = br.ReadSingle();
        _fadeEnd = br.ReadSingle();
        _decayRate = br.ReadInt16();
        br.Position += 2; // padding
        _intensity = br.ReadSingle();
        _shadowCaster = br.ReadBoolean();
        br.Position += 3; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSByte((sbyte)_type);
        for (int i = 0; i < 15; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _position);
        s.WriteVector4(bw, _direction);
        bw.WriteUInt32(_color);
        bw.WriteSingle(_angle);
        bw.WriteSingle(_range);
        bw.WriteSingle(_fadeStart);
        bw.WriteSingle(_fadeEnd);
        bw.WriteInt16(_decayRate);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_intensity);
        bw.WriteBoolean(_shadowCaster);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hkxLight_LightType, sbyte>(xe, nameof(_type), (sbyte)_type);
        xs.WriteVector4(xe, nameof(_position), _position);
        xs.WriteVector4(xe, nameof(_direction), _direction);
        xs.WriteNumber(xe, nameof(_color), _color);
        xs.WriteFloat(xe, nameof(_angle), _angle);
        xs.WriteFloat(xe, nameof(_range), _range);
        xs.WriteFloat(xe, nameof(_fadeStart), _fadeStart);
        xs.WriteFloat(xe, nameof(_fadeEnd), _fadeEnd);
        xs.WriteNumber(xe, nameof(_decayRate), _decayRate);
        xs.WriteFloat(xe, nameof(_intensity), _intensity);
        xs.WriteBoolean(xe, nameof(_shadowCaster), _shadowCaster);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxLight);
    }
    public bool Equals(hkxLight? other)
    {
        return other is not null && _type.Equals(other._type) && _position.Equals(other._position) && _direction.Equals(other._direction) && _color.Equals(other._color) && _angle.Equals(other._angle) && _range.Equals(other._range) && _fadeStart.Equals(other._fadeStart) && _fadeEnd.Equals(other._fadeEnd) && _decayRate.Equals(other._decayRate) && _intensity.Equals(other._intensity) && _shadowCaster.Equals(other._shadowCaster) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(_position);
        code.Add(_direction);
        code.Add(_color);
        code.Add(_angle);
        code.Add(_range);
        code.Add(_fadeStart);
        code.Add(_fadeEnd);
        code.Add(_decayRate);
        code.Add(_intensity);
        code.Add(_shadowCaster);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
