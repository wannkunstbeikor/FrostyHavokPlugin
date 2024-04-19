using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpSpringAction : hknpBinaryAction
{
    public override uint Signature => 0;
    public Vector4 _lastForce;
    public Vector4 _positionAinA;
    public Vector4 _positionBinB;
    public float _restLength;
    public float _strength;
    public float _damping;
    public bool _onCompression;
    public bool _onExtension;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _lastForce = des.ReadVector4(br);
        _positionAinA = des.ReadVector4(br);
        _positionBinB = des.ReadVector4(br);
        _restLength = br.ReadSingle();
        _strength = br.ReadSingle();
        _damping = br.ReadSingle();
        _onCompression = br.ReadBoolean();
        _onExtension = br.ReadBoolean();
        br.Position += 2; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4(bw, _lastForce);
        s.WriteVector4(bw, _positionAinA);
        s.WriteVector4(bw, _positionBinB);
        bw.WriteSingle(_restLength);
        bw.WriteSingle(_strength);
        bw.WriteSingle(_damping);
        bw.WriteBoolean(_onCompression);
        bw.WriteBoolean(_onExtension);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_lastForce), _lastForce);
        xs.WriteVector4(xe, nameof(_positionAinA), _positionAinA);
        xs.WriteVector4(xe, nameof(_positionBinB), _positionBinB);
        xs.WriteFloat(xe, nameof(_restLength), _restLength);
        xs.WriteFloat(xe, nameof(_strength), _strength);
        xs.WriteFloat(xe, nameof(_damping), _damping);
        xs.WriteBoolean(xe, nameof(_onCompression), _onCompression);
        xs.WriteBoolean(xe, nameof(_onExtension), _onExtension);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpSpringAction other && base.Equals(other) && _lastForce == other._lastForce && _positionAinA == other._positionAinA && _positionBinB == other._positionBinB && _restLength == other._restLength && _strength == other._strength && _damping == other._damping && _onCompression == other._onCompression && _onExtension == other._onExtension && Signature == other.Signature;
    }
    public static bool operator ==(hknpSpringAction? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpSpringAction? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_lastForce);
        code.Add(_positionAinA);
        code.Add(_positionBinB);
        code.Add(_restLength);
        code.Add(_strength);
        code.Add(_damping);
        code.Add(_onCompression);
        code.Add(_onExtension);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
