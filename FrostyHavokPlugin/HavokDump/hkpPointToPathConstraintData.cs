using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPointToPathConstraintData : hkpConstraintData
{
    public override uint Signature => 0;
    public hkpBridgeAtoms? _atoms;
    public hkpParametricCurve? _path;
    public float _maxFrictionForce;
    public hkpPointToPathConstraintData_OrientationConstraintType _angularConstrainedDOF;
    public Matrix4[] _transform_OS_KS = new Matrix4[2];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _atoms = new hkpBridgeAtoms();
        _atoms.Read(des, br);
        _path = des.ReadClassPointer<hkpParametricCurve>(br);
        _maxFrictionForce = br.ReadSingle();
        _angularConstrainedDOF = (hkpPointToPathConstraintData_OrientationConstraintType)br.ReadSByte();
        br.Position += 3; // padding
        _transform_OS_KS = des.ReadTransformCStyleArray(br, 2);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        _atoms.Write(s, bw);
        s.WriteClassPointer<hkpParametricCurve>(bw, _path);
        bw.WriteSingle(_maxFrictionForce);
        bw.WriteSByte((sbyte)_angularConstrainedDOF);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        s.WriteTransformCStyleArray(bw, _transform_OS_KS);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_atoms), _atoms);
        xs.WriteClassPointer(xe, nameof(_path), _path);
        xs.WriteFloat(xe, nameof(_maxFrictionForce), _maxFrictionForce);
        xs.WriteEnum<hkpPointToPathConstraintData_OrientationConstraintType, sbyte>(xe, nameof(_angularConstrainedDOF), (sbyte)_angularConstrainedDOF);
        xs.WriteTransformArray(xe, nameof(_transform_OS_KS), _transform_OS_KS);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpPointToPathConstraintData other && base.Equals(other) && _atoms == other._atoms && _path == other._path && _maxFrictionForce == other._maxFrictionForce && _angularConstrainedDOF == other._angularConstrainedDOF && _transform_OS_KS == other._transform_OS_KS && Signature == other.Signature;
    }
    public static bool operator ==(hkpPointToPathConstraintData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpPointToPathConstraintData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_atoms);
        code.Add(_path);
        code.Add(_maxFrictionForce);
        code.Add(_angularConstrainedDOF);
        code.Add(_transform_OS_KS);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
