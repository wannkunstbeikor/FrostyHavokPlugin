using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticPvsBlockHeader : IHavokObject, IEquatable<hkcdStaticPvsBlockHeader?>
{
    public virtual uint Signature => 0;
    public uint _offset;
    public uint _length;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _offset = br.ReadUInt32();
        _length = br.ReadUInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_offset);
        bw.WriteUInt32(_length);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_offset), _offset);
        xs.WriteNumber(xe, nameof(_length), _length);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticPvsBlockHeader);
    }
    public bool Equals(hkcdStaticPvsBlockHeader? other)
    {
        return other is not null && _offset.Equals(other._offset) && _length.Equals(other._length) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_offset);
        code.Add(_length);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
