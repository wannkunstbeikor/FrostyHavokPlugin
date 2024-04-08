using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpGroupCollisionFilter : hknpCollisionFilter, IEquatable<hknpGroupCollisionFilter?>
{
    public override uint Signature => 0;
    public int _nextFreeSystemGroup;
    public uint[] _collisionLookupTable = new uint[32];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _nextFreeSystemGroup = br.ReadInt32();
        _collisionLookupTable = des.ReadUInt32CStyleArray(br, 32);
        br.Position += 12; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_nextFreeSystemGroup);
        s.WriteUInt32CStyleArray(bw, _collisionLookupTable);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_nextFreeSystemGroup), _nextFreeSystemGroup);
        xs.WriteNumberArray(xe, nameof(_collisionLookupTable), _collisionLookupTable);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpGroupCollisionFilter);
    }
    public bool Equals(hknpGroupCollisionFilter? other)
    {
        return other is not null && _nextFreeSystemGroup.Equals(other._nextFreeSystemGroup) && _collisionLookupTable.Equals(other._collisionLookupTable) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nextFreeSystemGroup);
        code.Add(_collisionLookupTable);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
