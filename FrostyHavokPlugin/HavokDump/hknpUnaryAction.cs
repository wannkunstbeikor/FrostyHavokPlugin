using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpUnaryAction : hknpAction
{
    public override uint Signature => 0;
    public uint _body;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _body = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_body);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_body), _body);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpUnaryAction other && base.Equals(other) && _body == other._body && Signature == other.Signature;
    }
    public static bool operator ==(hknpUnaryAction? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpUnaryAction? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_body);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
