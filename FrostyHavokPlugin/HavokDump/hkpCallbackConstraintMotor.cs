using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpCallbackConstraintMotor : hkpLimitedForceConstraintMotor, IEquatable<hkpCallbackConstraintMotor?>
{
    public override uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _callbackFunc
    public hkpCallbackConstraintMotor_CallbackType _callbackType;
    public ulong _userData0;
    public ulong _userData1;
    public ulong _userData2;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _callbackType = (hkpCallbackConstraintMotor_CallbackType)br.ReadUInt32();
        br.Position += 4; // padding
        _userData0 = br.ReadUInt64();
        _userData1 = br.ReadUInt64();
        _userData2 = br.ReadUInt64();
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hkpCallbackConstraintMotor_CallbackType, uint>(xe, nameof(_callbackType), (uint)_callbackType);
        xs.WriteNumber(xe, nameof(_userData0), _userData0);
        xs.WriteNumber(xe, nameof(_userData1), _userData1);
        xs.WriteNumber(xe, nameof(_userData2), _userData2);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpCallbackConstraintMotor);
    }
    public bool Equals(hkpCallbackConstraintMotor? other)
    {
        return other is not null && _callbackType.Equals(other._callbackType) && _userData0.Equals(other._userData0) && _userData1.Equals(other._userData1) && _userData2.Equals(other._userData2) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_callbackType);
        code.Add(_userData0);
        code.Add(_userData1);
        code.Add(_userData2);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
