using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkAabb16 : IHavokObject
{
    public virtual uint Signature => 0;
    public ushort[] _min = new ushort[3];
    public ushort _key;
    public ushort[] _max = new ushort[3];
    public ushort _key1;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _min = des.ReadUInt16CStyleArray(br, 3);
        _key = br.ReadUInt16();
        _max = des.ReadUInt16CStyleArray(br, 3);
        _key1 = br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUInt16CStyleArray(bw, _min);
        bw.WriteUInt16(_key);
        s.WriteUInt16CStyleArray(bw, _max);
        bw.WriteUInt16(_key1);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_min), _min);
        xs.WriteNumber(xe, nameof(_key), _key);
        xs.WriteNumberArray(xe, nameof(_max), _max);
        xs.WriteNumber(xe, nameof(_key1), _key1);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkAabb16 other && _min == other._min && _key == other._key && _max == other._max && _key1 == other._key1 && Signature == other.Signature;
    }
    public static bool operator ==(hkAabb16? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkAabb16? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_min);
        code.Add(_key);
        code.Add(_max);
        code.Add(_key1);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
