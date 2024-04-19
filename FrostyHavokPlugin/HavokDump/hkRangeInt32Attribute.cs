using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkRangeInt32Attribute : IHavokObject
{
    public virtual uint Signature => 0;
    public int _absmin;
    public int _absmax;
    public int _softmin;
    public int _softmax;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _absmin = br.ReadInt32();
        _absmax = br.ReadInt32();
        _softmin = br.ReadInt32();
        _softmax = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_absmin);
        bw.WriteInt32(_absmax);
        bw.WriteInt32(_softmin);
        bw.WriteInt32(_softmax);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_absmin), _absmin);
        xs.WriteNumber(xe, nameof(_absmax), _absmax);
        xs.WriteNumber(xe, nameof(_softmin), _softmin);
        xs.WriteNumber(xe, nameof(_softmax), _softmax);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkRangeInt32Attribute other && _absmin == other._absmin && _absmax == other._absmax && _softmin == other._softmin && _softmax == other._softmax && Signature == other.Signature;
    }
    public static bool operator ==(hkRangeInt32Attribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkRangeInt32Attribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_absmin);
        code.Add(_absmax);
        code.Add(_softmin);
        code.Add(_softmax);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
