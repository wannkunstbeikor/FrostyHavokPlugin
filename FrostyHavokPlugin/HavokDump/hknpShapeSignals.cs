using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpShapeSignals : IHavokObject, IEquatable<hknpShapeSignals?>
{
    public virtual uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _shapeMutated
    // TYPE_POINTER TYPE_VOID _shapeDestroyed
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        br.Position += 16; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpShapeSignals);
    }
    public bool Equals(hknpShapeSignals? other)
    {
        return other is not null && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}
