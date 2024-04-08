using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkRangeRealAttribute : IHavokObject, IEquatable<hkRangeRealAttribute?>
{
    public virtual uint Signature => 0;
    public float _absmin;
    public float _absmax;
    public float _softmin;
    public float _softmax;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _absmin = br.ReadSingle();
        _absmax = br.ReadSingle();
        _softmin = br.ReadSingle();
        _softmax = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_absmin);
        bw.WriteSingle(_absmax);
        bw.WriteSingle(_softmin);
        bw.WriteSingle(_softmax);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_absmin), _absmin);
        xs.WriteFloat(xe, nameof(_absmax), _absmax);
        xs.WriteFloat(xe, nameof(_softmin), _softmin);
        xs.WriteFloat(xe, nameof(_softmax), _softmax);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkRangeRealAttribute);
    }
    public bool Equals(hkRangeRealAttribute? other)
    {
        return other is not null && _absmin.Equals(other._absmin) && _absmax.Equals(other._absmax) && _softmin.Equals(other._softmin) && _softmax.Equals(other._softmax) && Signature == other.Signature;
    }
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
