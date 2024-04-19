using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeCodec3Axis : IHavokObject
{
    public virtual uint Signature => 0;
    public byte[] _xyz = new byte[3];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _xyz = des.ReadByteCStyleArray(br, 3);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteByteCStyleArray(bw, _xyz);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_xyz), _xyz);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticTreeCodec3Axis other && _xyz == other._xyz && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticTreeCodec3Axis? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticTreeCodec3Axis? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_xyz);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
