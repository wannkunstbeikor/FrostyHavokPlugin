using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBodyReference : hkReferencedObject, IEquatable<hknpBodyReference?>
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
        return Equals(obj as hknpBodyReference);
    }
    public bool Equals(hknpBodyReference? other)
    {
        return other is not null && _id.Equals(other._id) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_id);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
