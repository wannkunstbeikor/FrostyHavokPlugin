using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeCodec3Axis6 : hkcdStaticTreeCodec3Axis, IEquatable<hkcdStaticTreeCodec3Axis6?>
{
    public override uint Signature => 0;
    public byte _hiData;
    public ushort _loData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _hiData = br.ReadByte();
        _loData = br.ReadUInt16();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_hiData);
        bw.WriteUInt16(_loData);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_hiData), _hiData);
        xs.WriteNumber(xe, nameof(_loData), _loData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticTreeCodec3Axis6);
    }
    public bool Equals(hkcdStaticTreeCodec3Axis6? other)
    {
        return other is not null && _hiData.Equals(other._hiData) && _loData.Equals(other._loData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_hiData);
        code.Add(_loData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
