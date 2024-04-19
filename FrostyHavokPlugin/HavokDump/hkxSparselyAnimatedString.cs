using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSparselyAnimatedString : hkReferencedObject
{
    public override uint Signature => 0;
    public List<string> _strings = new();
    public List<float> _times = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _strings = des.ReadStringPointerArray(br);
        _times = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointerArray(bw, _strings);
        s.WriteSingleArray(bw, _times);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteStringArray(xe, nameof(_strings), _strings);
        xs.WriteFloatArray(xe, nameof(_times), _times);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxSparselyAnimatedString other && base.Equals(other) && _strings.SequenceEqual(other._strings) && _times.SequenceEqual(other._times) && Signature == other.Signature;
    }
    public static bool operator ==(hkxSparselyAnimatedString? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxSparselyAnimatedString? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_strings);
        code.Add(_times);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
