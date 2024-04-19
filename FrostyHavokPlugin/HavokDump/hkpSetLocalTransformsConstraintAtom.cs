using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpSetLocalTransformsConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public Matrix4 _transformA;
    public Matrix4 _transformB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
        _transformA = des.ReadTransform(br);
        _transformB = des.ReadTransform(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
        s.WriteTransform(bw, _transformA);
        s.WriteTransform(bw, _transformB);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteTransform(xe, nameof(_transformA), _transformA);
        xs.WriteTransform(xe, nameof(_transformB), _transformB);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpSetLocalTransformsConstraintAtom other && base.Equals(other) && _transformA == other._transformA && _transformB == other._transformB && Signature == other.Signature;
    }
    public static bool operator ==(hkpSetLocalTransformsConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpSetLocalTransformsConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transformA);
        code.Add(_transformB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
