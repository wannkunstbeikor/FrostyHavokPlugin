using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkp2dAngConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public byte _freeRotationAxis;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _freeRotationAxis = br.ReadByte();
        br.Position += 13; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte(_freeRotationAxis);
        for (int i = 0; i < 13; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_freeRotationAxis), _freeRotationAxis);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkp2dAngConstraintAtom other && base.Equals(other) && _freeRotationAxis == other._freeRotationAxis && Signature == other.Signature;
    }
    public static bool operator ==(hkp2dAngConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkp2dAngConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_freeRotationAxis);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
