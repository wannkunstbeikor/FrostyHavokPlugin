using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMaterial : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public uint _isExclusive;
    public hknpMaterial_FlagsEnum _flags;
    public hknpMaterial_TriggerType _triggerType;
    public hkUFloat8? _triggerManifoldTolerance;
    public Half _dynamicFriction;
    public Half _staticFriction;
    public Half _restitution;
    public hknpMaterial_CombinePolicy _frictionCombinePolicy;
    public hknpMaterial_CombinePolicy _restitutionCombinePolicy;
    public Half _weldingTolerance;
    public float _maxContactImpulse;
    public float _fractionOfClippedImpulseToApply;
    public hknpMaterial_MassChangerCategory _massChangerCategory;
    public Half _massChangerHeavyObjectFactor;
    public Half _softContactForceFactor;
    public Half _softContactDampFactor;
    public hkUFloat8? _softContactSeperationVelocity;
    public hknpSurfaceVelocity? _surfaceVelocity;
    public Half _disablingCollisionsBetweenCvxCvxDynamicObjectsDistance;
    public ulong _userData;
    public bool _isShared;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _isExclusive = br.ReadUInt32();
        _flags = (hknpMaterial_FlagsEnum)br.ReadUInt32();
        _triggerType = (hknpMaterial_TriggerType)br.ReadByte();
        _triggerManifoldTolerance = new hkUFloat8();
        _triggerManifoldTolerance.Read(des, br);
        _dynamicFriction = des.ReadHalf(br);
        _staticFriction = des.ReadHalf(br);
        _restitution = des.ReadHalf(br);
        _frictionCombinePolicy = (hknpMaterial_CombinePolicy)br.ReadByte();
        _restitutionCombinePolicy = (hknpMaterial_CombinePolicy)br.ReadByte();
        _weldingTolerance = des.ReadHalf(br);
        _maxContactImpulse = br.ReadSingle();
        _fractionOfClippedImpulseToApply = br.ReadSingle();
        _massChangerCategory = (hknpMaterial_MassChangerCategory)br.ReadByte();
        br.Position += 1; // padding
        _massChangerHeavyObjectFactor = des.ReadHalf(br);
        _softContactForceFactor = des.ReadHalf(br);
        _softContactDampFactor = des.ReadHalf(br);
        _softContactSeperationVelocity = new hkUFloat8();
        _softContactSeperationVelocity.Read(des, br);
        br.Position += 3; // padding
        _surfaceVelocity = des.ReadClassPointer<hknpSurfaceVelocity>(br);
        _disablingCollisionsBetweenCvxCvxDynamicObjectsDistance = des.ReadHalf(br);
        br.Position += 6; // padding
        _userData = br.ReadUInt64();
        _isShared = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        bw.WriteUInt32(_isExclusive);
        bw.WriteUInt32((uint)_flags);
        bw.WriteByte((byte)_triggerType);
        _triggerManifoldTolerance.Write(s, bw);
        s.WriteHalf(bw, _dynamicFriction);
        s.WriteHalf(bw, _staticFriction);
        s.WriteHalf(bw, _restitution);
        bw.WriteByte((byte)_frictionCombinePolicy);
        bw.WriteByte((byte)_restitutionCombinePolicy);
        s.WriteHalf(bw, _weldingTolerance);
        bw.WriteSingle(_maxContactImpulse);
        bw.WriteSingle(_fractionOfClippedImpulseToApply);
        bw.WriteByte((byte)_massChangerCategory);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        s.WriteHalf(bw, _massChangerHeavyObjectFactor);
        s.WriteHalf(bw, _softContactForceFactor);
        s.WriteHalf(bw, _softContactDampFactor);
        _softContactSeperationVelocity.Write(s, bw);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hknpSurfaceVelocity>(bw, _surfaceVelocity);
        s.WriteHalf(bw, _disablingCollisionsBetweenCvxCvxDynamicObjectsDistance);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
        bw.WriteUInt64(_userData);
        bw.WriteBoolean(_isShared);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteNumber(xe, nameof(_isExclusive), _isExclusive);
        xs.WriteFlag<hknpMaterial_FlagsEnum, uint>(xe, nameof(_flags), (uint)_flags);
        xs.WriteEnum<hknpMaterial_TriggerType, byte>(xe, nameof(_triggerType), (byte)_triggerType);
        xs.WriteClass(xe, nameof(_triggerManifoldTolerance), _triggerManifoldTolerance);
        xs.WriteFloat(xe, nameof(_dynamicFriction), _dynamicFriction);
        xs.WriteFloat(xe, nameof(_staticFriction), _staticFriction);
        xs.WriteFloat(xe, nameof(_restitution), _restitution);
        xs.WriteEnum<hknpMaterial_CombinePolicy, byte>(xe, nameof(_frictionCombinePolicy), (byte)_frictionCombinePolicy);
        xs.WriteEnum<hknpMaterial_CombinePolicy, byte>(xe, nameof(_restitutionCombinePolicy), (byte)_restitutionCombinePolicy);
        xs.WriteFloat(xe, nameof(_weldingTolerance), _weldingTolerance);
        xs.WriteFloat(xe, nameof(_maxContactImpulse), _maxContactImpulse);
        xs.WriteFloat(xe, nameof(_fractionOfClippedImpulseToApply), _fractionOfClippedImpulseToApply);
        xs.WriteEnum<hknpMaterial_MassChangerCategory, byte>(xe, nameof(_massChangerCategory), (byte)_massChangerCategory);
        xs.WriteFloat(xe, nameof(_massChangerHeavyObjectFactor), _massChangerHeavyObjectFactor);
        xs.WriteFloat(xe, nameof(_softContactForceFactor), _softContactForceFactor);
        xs.WriteFloat(xe, nameof(_softContactDampFactor), _softContactDampFactor);
        xs.WriteClass(xe, nameof(_softContactSeperationVelocity), _softContactSeperationVelocity);
        xs.WriteClassPointer(xe, nameof(_surfaceVelocity), _surfaceVelocity);
        xs.WriteFloat(xe, nameof(_disablingCollisionsBetweenCvxCvxDynamicObjectsDistance), _disablingCollisionsBetweenCvxCvxDynamicObjectsDistance);
        xs.WriteNumber(xe, nameof(_userData), _userData);
        xs.WriteBoolean(xe, nameof(_isShared), _isShared);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpMaterial other && _name == other._name && _isExclusive == other._isExclusive && _flags == other._flags && _triggerType == other._triggerType && _triggerManifoldTolerance == other._triggerManifoldTolerance && _dynamicFriction == other._dynamicFriction && _staticFriction == other._staticFriction && _restitution == other._restitution && _frictionCombinePolicy == other._frictionCombinePolicy && _restitutionCombinePolicy == other._restitutionCombinePolicy && _weldingTolerance == other._weldingTolerance && _maxContactImpulse == other._maxContactImpulse && _fractionOfClippedImpulseToApply == other._fractionOfClippedImpulseToApply && _massChangerCategory == other._massChangerCategory && _massChangerHeavyObjectFactor == other._massChangerHeavyObjectFactor && _softContactForceFactor == other._softContactForceFactor && _softContactDampFactor == other._softContactDampFactor && _softContactSeperationVelocity == other._softContactSeperationVelocity && _surfaceVelocity == other._surfaceVelocity && _disablingCollisionsBetweenCvxCvxDynamicObjectsDistance == other._disablingCollisionsBetweenCvxCvxDynamicObjectsDistance && _userData == other._userData && _isShared == other._isShared && Signature == other.Signature;
    }
    public static bool operator ==(hknpMaterial? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMaterial? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_isExclusive);
        code.Add(_flags);
        code.Add(_triggerType);
        code.Add(_triggerManifoldTolerance);
        code.Add(_dynamicFriction);
        code.Add(_staticFriction);
        code.Add(_restitution);
        code.Add(_frictionCombinePolicy);
        code.Add(_restitutionCombinePolicy);
        code.Add(_weldingTolerance);
        code.Add(_maxContactImpulse);
        code.Add(_fractionOfClippedImpulseToApply);
        code.Add(_massChangerCategory);
        code.Add(_massChangerHeavyObjectFactor);
        code.Add(_softContactForceFactor);
        code.Add(_softContactDampFactor);
        code.Add(_softContactSeperationVelocity);
        code.Add(_surfaceVelocity);
        code.Add(_disablingCollisionsBetweenCvxCvxDynamicObjectsDistance);
        code.Add(_userData);
        code.Add(_isShared);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
