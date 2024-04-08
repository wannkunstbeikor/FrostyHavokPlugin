using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCollisionFilter : hkReferencedObject, IEquatable<hknpCollisionFilter?>
{
    public override uint Signature => 0;
    public hknpCollisionFilter_FilterType _type;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _type = (hknpCollisionFilter_FilterType)br.ReadByte();
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
        xs.WriteEnum<hknpCollisionFilter_FilterType, byte>(xe, nameof(_type), (byte)_type);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCollisionFilter);
    }
    public bool Equals(hknpCollisionFilter? other)
    {
        return other is not null && _type.Equals(other._type) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
