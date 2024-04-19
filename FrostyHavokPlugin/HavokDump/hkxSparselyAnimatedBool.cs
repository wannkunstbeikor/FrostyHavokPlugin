using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSparselyAnimatedBool : hkReferencedObject
{
    public override uint Signature => 0;
    public List<bool> _bools = new();
    public List<float> _times = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _bools = des.ReadBooleanArray(br);
        _times = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteBooleanArray(bw, _bools);
        s.WriteSingleArray(bw, _times);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBooleanArray(xe, nameof(_bools), _bools);
        xs.WriteFloatArray(xe, nameof(_times), _times);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxSparselyAnimatedBool other && base.Equals(other) && _bools.SequenceEqual(other._bools) && _times.SequenceEqual(other._times) && Signature == other.Signature;
    }
    public static bool operator ==(hkxSparselyAnimatedBool? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxSparselyAnimatedBool? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bools);
        code.Add(_times);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
