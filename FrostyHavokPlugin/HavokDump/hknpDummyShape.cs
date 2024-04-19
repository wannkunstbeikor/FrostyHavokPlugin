using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDummyShape : hknpShape
{
    public override uint Signature => 0;
    public hkAabb? _aabb;
    // TYPE_STRUCT TYPE_VOID _signals
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _aabb = new hkAabb();
        _aabb.Read(des, br);
        br.Position += 16; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _aabb.Write(s, bw);
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_aabb), _aabb);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpDummyShape other && base.Equals(other) && _aabb == other._aabb && Signature == other.Signature;
    }
    public static bool operator ==(hknpDummyShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpDummyShape? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
