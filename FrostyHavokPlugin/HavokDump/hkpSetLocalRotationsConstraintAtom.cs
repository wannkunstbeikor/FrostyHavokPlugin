using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpSetLocalRotationsConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public Matrix3x4 _rotationA;
    public Matrix3x4 _rotationB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
        _rotationA = des.ReadMatrix3(br);
        _rotationB = des.ReadMatrix3(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
        s.WriteMatrix3(bw, _rotationA);
        s.WriteMatrix3(bw, _rotationB);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteMatrix3(xe, nameof(_rotationA), _rotationA);
        xs.WriteMatrix3(xe, nameof(_rotationB), _rotationB);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpSetLocalRotationsConstraintAtom other && base.Equals(other) && _rotationA == other._rotationA && _rotationB == other._rotationB && Signature == other.Signature;
    }
    public static bool operator ==(hkpSetLocalRotationsConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpSetLocalRotationsConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_rotationA);
        code.Add(_rotationB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
