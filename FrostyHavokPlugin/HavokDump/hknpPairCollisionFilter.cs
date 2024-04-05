using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpPairCollisionFilter : hknpCollisionFilter, IEquatable<hknpPairCollisionFilter?>
{
    public override uint Signature => 0;
    // TYPE_STRUCT TYPE_VOID _disabledPairs
    public hknpCollisionFilter _childFilter;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 16; // padding
        _childFilter = des.ReadClassPointer<hknpCollisionFilter>(br);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_childFilter), _childFilter);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpPairCollisionFilter);
    }
    public bool Equals(hknpPairCollisionFilter? other)
    {
        return other is not null && _childFilter.Equals(other._childFilter) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_childFilter);
        code.Add(Signature);
        return code.ToHashCode();
    }
}