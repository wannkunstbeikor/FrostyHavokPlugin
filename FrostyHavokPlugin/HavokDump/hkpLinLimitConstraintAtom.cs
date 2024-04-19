using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLinLimitConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public byte _axisIndex;
    public float _min;
    public float _max;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _axisIndex = br.ReadByte();
        br.Position += 1; // padding
        _min = br.ReadSingle();
        _max = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_axisIndex);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_min);
        bw.WriteSingle(_max);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_axisIndex), _axisIndex);
        xs.WriteFloat(xe, nameof(_min), _min);
        xs.WriteFloat(xe, nameof(_max), _max);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpLinLimitConstraintAtom other && base.Equals(other) && _axisIndex == other._axisIndex && _min == other._min && _max == other._max && Signature == other.Signature;
    }
    public static bool operator ==(hkpLinLimitConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpLinLimitConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_axisIndex);
        code.Add(_min);
        code.Add(_max);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
