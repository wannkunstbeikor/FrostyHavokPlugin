using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultVelocityDamper : hknpVehicleVelocityDamper
{
    public override uint Signature => 0;
    public float _normalSpinDamping;
    public float _collisionSpinDamping;
    public float _collisionThreshold;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _normalSpinDamping = br.ReadSingle();
        _collisionSpinDamping = br.ReadSingle();
        _collisionThreshold = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_normalSpinDamping);
        bw.WriteSingle(_collisionSpinDamping);
        bw.WriteSingle(_collisionThreshold);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_normalSpinDamping), _normalSpinDamping);
        xs.WriteFloat(xe, nameof(_collisionSpinDamping), _collisionSpinDamping);
        xs.WriteFloat(xe, nameof(_collisionThreshold), _collisionThreshold);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleDefaultVelocityDamper other && base.Equals(other) && _normalSpinDamping == other._normalSpinDamping && _collisionSpinDamping == other._collisionSpinDamping && _collisionThreshold == other._collisionThreshold && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleDefaultVelocityDamper? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleDefaultVelocityDamper? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_normalSpinDamping);
        code.Add(_collisionSpinDamping);
        code.Add(_collisionThreshold);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
