using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBroadPhaseConfigLayer : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _collideWithLayerMask;
    public bool _isVolatile;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _collideWithLayerMask = br.ReadUInt32();
        _isVolatile = br.ReadBoolean();
        br.Position += 3; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_collideWithLayerMask);
        bw.WriteBoolean(_isVolatile);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_collideWithLayerMask), _collideWithLayerMask);
        xs.WriteBoolean(xe, nameof(_isVolatile), _isVolatile);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpBroadPhaseConfigLayer other && _collideWithLayerMask == other._collideWithLayerMask && _isVolatile == other._isVolatile && Signature == other.Signature;
    }
    public static bool operator ==(hknpBroadPhaseConfigLayer? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpBroadPhaseConfigLayer? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_collideWithLayerMask);
        code.Add(_isVolatile);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
