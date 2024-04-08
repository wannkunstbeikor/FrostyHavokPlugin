using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSparselyAnimatedInt : hkReferencedObject, IEquatable<hkxSparselyAnimatedInt?>
{
    public override uint Signature => 0;
    public List<int> _ints;
    public List<float> _times;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _ints = des.ReadInt32Array(br);
        _times = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteInt32Array(bw, _ints);
        s.WriteSingleArray(bw, _times);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_ints), _ints);
        xs.WriteFloatArray(xe, nameof(_times), _times);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxSparselyAnimatedInt);
    }
    public bool Equals(hkxSparselyAnimatedInt? other)
    {
        return other is not null && _ints.Equals(other._ints) && _times.Equals(other._times) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_ints);
        code.Add(_times);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
