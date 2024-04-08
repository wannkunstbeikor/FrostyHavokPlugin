using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSplineControlPoint : IHavokObject, IEquatable<hkxSplineControlPoint?>
{
    public virtual uint Signature => 0;
    public Vector4 _position;
    public Vector4 _tangentIn;
    public Vector4 _tangentOut;
    public hkxSpline_ControlType _inType;
    public hkxSpline_ControlType _outType;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _position = des.ReadVector4(br);
        _tangentIn = des.ReadVector4(br);
        _tangentOut = des.ReadVector4(br);
        _inType = (hkxSpline_ControlType)br.ReadByte();
        _outType = (hkxSpline_ControlType)br.ReadByte();
        br.Position += 14; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _position);
        s.WriteVector4(bw, _tangentIn);
        s.WriteVector4(bw, _tangentOut);
        bw.WriteByte((byte)_inType);
        bw.WriteByte((byte)_outType);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_position), _position);
        xs.WriteVector4(xe, nameof(_tangentIn), _tangentIn);
        xs.WriteVector4(xe, nameof(_tangentOut), _tangentOut);
        xs.WriteEnum<hkxSpline_ControlType, byte>(xe, nameof(_inType), (byte)_inType);
        xs.WriteEnum<hkxSpline_ControlType, byte>(xe, nameof(_outType), (byte)_outType);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxSplineControlPoint);
    }
    public bool Equals(hkxSplineControlPoint? other)
    {
        return other is not null && _position.Equals(other._position) && _tangentIn.Equals(other._tangentIn) && _tangentOut.Equals(other._tangentOut) && _inType.Equals(other._inType) && _outType.Equals(other._outType) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_position);
        code.Add(_tangentIn);
        code.Add(_tangentOut);
        code.Add(_inType);
        code.Add(_outType);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
