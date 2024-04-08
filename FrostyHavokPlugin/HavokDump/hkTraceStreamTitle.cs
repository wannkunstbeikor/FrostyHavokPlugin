using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkTraceStreamTitle : IHavokObject, IEquatable<hkTraceStreamTitle?>
{
    public virtual uint Signature => 0;
    public sbyte[] _value = new sbyte[32];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _value = des.ReadSByteCStyleArray(br, 32);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteSByteCStyleArray(bw, _value);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_value), _value);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkTraceStreamTitle);
    }
    public bool Equals(hkTraceStreamTitle? other)
    {
        return other is not null && _value.Equals(other._value) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
