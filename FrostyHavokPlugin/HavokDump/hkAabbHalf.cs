using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkAabbHalf : IHavokObject
{
    public virtual uint Signature => 0;
    public ushort[] _data = new ushort[8];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _data = des.ReadUInt16CStyleArray(br, 8);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUInt16CStyleArray(bw, _data);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_data), _data);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkAabbHalf other && _data == other._data && Signature == other.Signature;
    }
    public static bool operator ==(hkAabbHalf? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkAabbHalf? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
