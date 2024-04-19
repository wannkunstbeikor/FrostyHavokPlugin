using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpShapeTagCodec : hkReferencedObject
{
    public override uint Signature => 0;
    public hknpShapeTagCodec_CodecType _type;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _type = (hknpShapeTagCodec_CodecType)br.ReadByte();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte((byte)_type);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hknpShapeTagCodec_CodecType, byte>(xe, nameof(_type), (byte)_type);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpShapeTagCodec other && base.Equals(other) && _type == other._type && Signature == other.Signature;
    }
    public static bool operator ==(hknpShapeTagCodec? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpShapeTagCodec? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
