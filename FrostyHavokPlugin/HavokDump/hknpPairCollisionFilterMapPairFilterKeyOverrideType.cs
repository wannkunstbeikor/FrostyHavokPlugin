using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpPairCollisionFilterMapPairFilterKeyOverrideType : IHavokObject, IEquatable<hknpPairCollisionFilterMapPairFilterKeyOverrideType?>
{
    public virtual uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _elem
    public int _numElems;
    public int _hashMod;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        br.Position += 8; // padding
        _numElems = br.ReadInt32();
        _hashMod = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numElems);
        bw.WriteInt32(_hashMod);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_numElems), _numElems);
        xs.WriteNumber(xe, nameof(_hashMod), _hashMod);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpPairCollisionFilterMapPairFilterKeyOverrideType);
    }
    public bool Equals(hknpPairCollisionFilterMapPairFilterKeyOverrideType? other)
    {
        return other is not null && _numElems.Equals(other._numElems) && _hashMod.Equals(other._hashMod) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_numElems);
        code.Add(_hashMod);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
