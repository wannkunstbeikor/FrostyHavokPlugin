using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeCodec3Axis4 : hkcdStaticTreeCodec3Axis
{
    public override uint Signature => 0;
    public byte _data;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _data = br.ReadByte();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_data);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_data), _data);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticTreeCodec3Axis4 other && base.Equals(other) && _data == other._data && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticTreeCodec3Axis4? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticTreeCodec3Axis4? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
