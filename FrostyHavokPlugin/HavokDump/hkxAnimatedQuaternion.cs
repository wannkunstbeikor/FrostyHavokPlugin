using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAnimatedQuaternion : hkReferencedObject
{
    public override uint Signature => 0;
    public List<float> _quaternions = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _quaternions = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteSingleArray(bw, _quaternions);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloatArray(xe, nameof(_quaternions), _quaternions);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxAnimatedQuaternion other && base.Equals(other) && _quaternions.SequenceEqual(other._quaternions) && Signature == other.Signature;
    }
    public static bool operator ==(hkxAnimatedQuaternion? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxAnimatedQuaternion? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_quaternions);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
