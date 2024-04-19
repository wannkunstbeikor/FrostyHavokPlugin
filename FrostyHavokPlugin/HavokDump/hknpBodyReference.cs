using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBodyReference : hkReferencedObject
{
    public override uint Signature => 0;
    public uint _id;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _id = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_id);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_id), _id);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpBodyReference other && base.Equals(other) && _id == other._id && Signature == other.Signature;
    }
    public static bool operator ==(hknpBodyReference? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpBodyReference? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_id);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
