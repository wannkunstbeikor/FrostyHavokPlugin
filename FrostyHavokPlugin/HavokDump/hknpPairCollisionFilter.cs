using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpPairCollisionFilter : hknpCollisionFilter
{
    public override uint Signature => 0;
    // TYPE_STRUCT TYPE_VOID _disabledPairs
    public hknpCollisionFilter? _childFilter;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 16; // padding
        _childFilter = des.ReadClassPointer<hknpCollisionFilter>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hknpCollisionFilter>(bw, _childFilter);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_childFilter), _childFilter);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpPairCollisionFilter other && base.Equals(other) && _childFilter == other._childFilter && Signature == other.Signature;
    }
    public static bool operator ==(hknpPairCollisionFilter? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpPairCollisionFilter? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_childFilter);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
