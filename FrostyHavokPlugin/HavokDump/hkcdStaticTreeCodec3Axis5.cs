using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeCodec3Axis5 : hkcdStaticTreeCodec3Axis
{
    public override uint Signature => 0;
    public byte _hiData;
    public byte _loData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _hiData = br.ReadByte();
        _loData = br.ReadByte();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_hiData);
        bw.WriteByte(_loData);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_hiData), _hiData);
        xs.WriteNumber(xe, nameof(_loData), _loData);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticTreeCodec3Axis5 other && base.Equals(other) && _hiData == other._hiData && _loData == other._loData && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticTreeCodec3Axis5? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticTreeCodec3Axis5? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_hiData);
        code.Add(_loData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
