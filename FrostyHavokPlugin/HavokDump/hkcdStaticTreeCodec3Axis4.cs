using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeCodec3Axis4 : hkcdStaticTreeCodec3Axis, IEquatable<hkcdStaticTreeCodec3Axis4?>
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
        return Equals(obj as hkcdStaticTreeCodec3Axis4);
    }
    public bool Equals(hkcdStaticTreeCodec3Axis4? other)
    {
        return other is not null && _data.Equals(other._data) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
