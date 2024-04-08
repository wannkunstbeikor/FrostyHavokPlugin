using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSparselyAnimatedEnum : hkxSparselyAnimatedInt, IEquatable<hkxSparselyAnimatedEnum?>
{
    public override uint Signature => 0;
    public hkxEnum _enum;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _enum = des.ReadClassPointer<hkxEnum>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkxEnum>(bw, _enum);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_enum), _enum);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxSparselyAnimatedEnum);
    }
    public bool Equals(hkxSparselyAnimatedEnum? other)
    {
        return other is not null && _enum.Equals(other._enum) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_enum);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
