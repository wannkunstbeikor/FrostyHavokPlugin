using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexIntDataChannel : hkReferencedObject
{
    public override uint Signature => 0;
    public List<int> _perVertexInts = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _perVertexInts = des.ReadInt32Array(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteInt32Array(bw, _perVertexInts);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_perVertexInts), _perVertexInts);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxVertexIntDataChannel other && base.Equals(other) && _perVertexInts.SequenceEqual(other._perVertexInts) && Signature == other.Signature;
    }
    public static bool operator ==(hkxVertexIntDataChannel? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxVertexIntDataChannel? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_perVertexInts);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
