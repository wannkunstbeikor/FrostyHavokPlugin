using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPulleyConstraintAtom : hkpConstraintAtom, IEquatable<hkpPulleyConstraintAtom?>
{
    public override uint Signature => 0;
    public Vector4 _fixedPivotAinWorld;
    public Vector4 _fixedPivotBinWorld;
    public float _ropeLength;
    public float _leverageOnBodyB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 14; // padding
        _fixedPivotAinWorld = des.ReadVector4(br);
        _fixedPivotBinWorld = des.ReadVector4(br);
        _ropeLength = br.ReadSingle();
        _leverageOnBodyB = br.ReadSingle();
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _fixedPivotAinWorld);
        s.WriteVector4(bw, _fixedPivotBinWorld);
        bw.WriteSingle(_ropeLength);
        bw.WriteSingle(_leverageOnBodyB);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_fixedPivotAinWorld), _fixedPivotAinWorld);
        xs.WriteVector4(xe, nameof(_fixedPivotBinWorld), _fixedPivotBinWorld);
        xs.WriteFloat(xe, nameof(_ropeLength), _ropeLength);
        xs.WriteFloat(xe, nameof(_leverageOnBodyB), _leverageOnBodyB);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpPulleyConstraintAtom);
    }
    public bool Equals(hkpPulleyConstraintAtom? other)
    {
        return other is not null && _fixedPivotAinWorld.Equals(other._fixedPivotAinWorld) && _fixedPivotBinWorld.Equals(other._fixedPivotBinWorld) && _ropeLength.Equals(other._ropeLength) && _leverageOnBodyB.Equals(other._leverageOnBodyB) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_fixedPivotAinWorld);
        code.Add(_fixedPivotBinWorld);
        code.Add(_ropeLength);
        code.Add(_leverageOnBodyB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
