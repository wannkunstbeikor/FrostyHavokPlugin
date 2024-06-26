using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpConstraintMotor : hkReferencedObject
{
    public override uint Signature => 0;
    public hkpConstraintMotor_MotorType _type;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _type = (hkpConstraintMotor_MotorType)br.ReadSByte();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSByte((sbyte)_type);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hkpConstraintMotor_MotorType, sbyte>(xe, nameof(_type), (sbyte)_type);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpConstraintMotor other && base.Equals(other) && _type == other._type && Signature == other.Signature;
    }
    public static bool operator ==(hkpConstraintMotor? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpConstraintMotor? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
