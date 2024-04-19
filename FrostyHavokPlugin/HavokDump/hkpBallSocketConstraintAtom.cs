using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpBallSocketConstraintAtom : hkpConstraintAtom
{
    public override uint Signature => 0;
    public hkpConstraintAtom_SolvingMethod _solvingMethod;
    public byte _bodiesToNotify;
    public hkUFloat8? _velocityStabilizationFactor;
    public bool _enableLinearImpulseLimit;
    public float _breachImpulse;
    public float _inertiaStabilizationFactor;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _solvingMethod = (hkpConstraintAtom_SolvingMethod)br.ReadByte();
        _bodiesToNotify = br.ReadByte();
        _velocityStabilizationFactor = new hkUFloat8();
        _velocityStabilizationFactor.Read(des, br);
        _enableLinearImpulseLimit = br.ReadBoolean();
        br.Position += 2; // padding
        _breachImpulse = br.ReadSingle();
        _inertiaStabilizationFactor = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteByte((byte)_solvingMethod);
        bw.WriteByte(_bodiesToNotify);
        _velocityStabilizationFactor.Write(s, bw);
        bw.WriteBoolean(_enableLinearImpulseLimit);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_breachImpulse);
        bw.WriteSingle(_inertiaStabilizationFactor);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hkpConstraintAtom_SolvingMethod, byte>(xe, nameof(_solvingMethod), (byte)_solvingMethod);
        xs.WriteNumber(xe, nameof(_bodiesToNotify), _bodiesToNotify);
        xs.WriteClass(xe, nameof(_velocityStabilizationFactor), _velocityStabilizationFactor);
        xs.WriteBoolean(xe, nameof(_enableLinearImpulseLimit), _enableLinearImpulseLimit);
        xs.WriteFloat(xe, nameof(_breachImpulse), _breachImpulse);
        xs.WriteFloat(xe, nameof(_inertiaStabilizationFactor), _inertiaStabilizationFactor);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpBallSocketConstraintAtom other && base.Equals(other) && _solvingMethod == other._solvingMethod && _bodiesToNotify == other._bodiesToNotify && _velocityStabilizationFactor == other._velocityStabilizationFactor && _enableLinearImpulseLimit == other._enableLinearImpulseLimit && _breachImpulse == other._breachImpulse && _inertiaStabilizationFactor == other._inertiaStabilizationFactor && Signature == other.Signature;
    }
    public static bool operator ==(hkpBallSocketConstraintAtom? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpBallSocketConstraintAtom? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_solvingMethod);
        code.Add(_bodiesToNotify);
        code.Add(_velocityStabilizationFactor);
        code.Add(_enableLinearImpulseLimit);
        code.Add(_breachImpulse);
        code.Add(_inertiaStabilizationFactor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
