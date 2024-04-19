using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConstraintCollisionFilter : hknpPairCollisionFilter
{
    public override uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _subscribedWorld
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpConstraintCollisionFilter other && base.Equals(other) && Signature == other.Signature;
    }
    public static bool operator ==(hknpConstraintCollisionFilter? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpConstraintCollisionFilter? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}
