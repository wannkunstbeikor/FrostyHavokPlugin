using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSparselyAnimatedBool : hkReferencedObject, IEquatable<hkxSparselyAnimatedBool?>
{
    public override uint Signature => 0;
    public List<bool> _bools;
    public List<float> _times;
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
        return Equals(obj as hkxSparselyAnimatedBool);
    }
    public bool Equals(hkxSparselyAnimatedBool? other)
    {
        return other is not null && _bools.Equals(other._bools) && _times.Equals(other._times) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bools);
        code.Add(_times);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
