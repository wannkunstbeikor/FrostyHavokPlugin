using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class HavokPhysicsContainer : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hknpShape?> _shapes = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _shapes = des.ReadClassPointerArray<hknpShape>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointerArray<hknpShape>(bw, _shapes);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointerArray<hknpShape>(xe, nameof(_shapes), _shapes);
    }
    public override bool Equals(object? obj)
    {
        return obj is HavokPhysicsContainer other && base.Equals(other) && _shapes.SequenceEqual(other._shapes) && Signature == other.Signature;
    }
    public static bool operator ==(HavokPhysicsContainer? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(HavokPhysicsContainer? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_shapes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
