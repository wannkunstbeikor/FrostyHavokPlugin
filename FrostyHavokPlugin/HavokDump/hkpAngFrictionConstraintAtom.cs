using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpAngFrictionConstraintAtom : hkpConstraintAtom, IEquatable<hkpAngFrictionConstraintAtom?>
{
    public override uint Signature => 0;
    public byte _isEnabled;
    public byte _firstFrictionAxis;
    public byte _numFrictionAxes;
    public float _maxFrictionTorque;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadByte();
        _firstFrictionAxis = br.ReadByte();
        _numFrictionAxes = br.ReadByte();
        br.Position += 3; // padding
        _maxFrictionTorque = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_isEnabled);
        bw.WriteByte(_firstFrictionAxis);
        bw.WriteByte(_numFrictionAxes);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_maxFrictionTorque);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_firstFrictionAxis), _firstFrictionAxis);
        xs.WriteNumber(xe, nameof(_numFrictionAxes), _numFrictionAxes);
        xs.WriteFloat(xe, nameof(_maxFrictionTorque), _maxFrictionTorque);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpAngFrictionConstraintAtom);
    }
    public bool Equals(hkpAngFrictionConstraintAtom? other)
    {
        return other is not null && _isEnabled.Equals(other._isEnabled) && _firstFrictionAxis.Equals(other._firstFrictionAxis) && _numFrictionAxes.Equals(other._numFrictionAxes) && _maxFrictionTorque.Equals(other._maxFrictionTorque) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_firstFrictionAxis);
        code.Add(_numFrictionAxes);
        code.Add(_maxFrictionTorque);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
