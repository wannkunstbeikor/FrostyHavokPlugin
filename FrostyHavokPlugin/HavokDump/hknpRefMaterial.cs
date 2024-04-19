using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpRefMaterial : hkReferencedObject
{
    public override uint Signature => 0;
    public hknpMaterial? _material;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _material = new hknpMaterial();
        _material.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _material.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_material), _material);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpRefMaterial other && base.Equals(other) && _material == other._material && Signature == other.Signature;
    }
    public static bool operator ==(hknpRefMaterial? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpRefMaterial? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_material);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
