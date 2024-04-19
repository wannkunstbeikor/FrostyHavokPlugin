using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWheelConstraintData : hkpConstraintData
{
    public override uint Signature => 0;
    public hkpWheelConstraintDataAtoms? _atoms;
    public Vector4 _initialAxleInB;
    public Vector4 _initialSteeringAxisInB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _atoms = new hkpWheelConstraintDataAtoms();
        _atoms.Read(des, br);
        _initialAxleInB = des.ReadVector4(br);
        _initialSteeringAxisInB = des.ReadVector4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        _atoms.Write(s, bw);
        s.WriteVector4(bw, _initialAxleInB);
        s.WriteVector4(bw, _initialSteeringAxisInB);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_atoms), _atoms);
        xs.WriteVector4(xe, nameof(_initialAxleInB), _initialAxleInB);
        xs.WriteVector4(xe, nameof(_initialSteeringAxisInB), _initialSteeringAxisInB);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpWheelConstraintData other && base.Equals(other) && _atoms == other._atoms && _initialAxleInB == other._initialAxleInB && _initialSteeringAxisInB == other._initialSteeringAxisInB && Signature == other.Signature;
    }
    public static bool operator ==(hkpWheelConstraintData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpWheelConstraintData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_atoms);
        code.Add(_initialAxleInB);
        code.Add(_initialSteeringAxisInB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
