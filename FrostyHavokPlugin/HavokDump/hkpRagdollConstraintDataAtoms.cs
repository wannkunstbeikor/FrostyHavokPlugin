using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRagdollConstraintDataAtoms : IHavokObject, IEquatable<hkpRagdollConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpSetupStabilizationAtom _setupStabilization;
    public hkpRagdollMotorConstraintAtom _ragdollMotors;
    public hkpAngFrictionConstraintAtom _angFriction;
    public hkpTwistLimitConstraintAtom _twistLimit;
    public hkpConeLimitConstraintAtom _coneLimit;
    public hkpConeLimitConstraintAtom _planesLimit;
    public hkpBallSocketConstraintAtom _ballSocket;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _setupStabilization = new hkpSetupStabilizationAtom();
        _setupStabilization.Read(des, br);
        _ragdollMotors = new hkpRagdollMotorConstraintAtom();
        _ragdollMotors.Read(des, br);
        _angFriction = new hkpAngFrictionConstraintAtom();
        _angFriction.Read(des, br);
        _twistLimit = new hkpTwistLimitConstraintAtom();
        _twistLimit.Read(des, br);
        _coneLimit = new hkpConeLimitConstraintAtom();
        _coneLimit.Read(des, br);
        _planesLimit = new hkpConeLimitConstraintAtom();
        _planesLimit.Read(des, br);
        _ballSocket = new hkpBallSocketConstraintAtom();
        _ballSocket.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _setupStabilization.Write(s, bw);
        _ragdollMotors.Write(s, bw);
        _angFriction.Write(s, bw);
        _twistLimit.Write(s, bw);
        _coneLimit.Write(s, bw);
        _planesLimit.Write(s, bw);
        _ballSocket.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_setupStabilization), _setupStabilization);
        xs.WriteClass(xe, nameof(_ragdollMotors), _ragdollMotors);
        xs.WriteClass(xe, nameof(_angFriction), _angFriction);
        xs.WriteClass(xe, nameof(_twistLimit), _twistLimit);
        xs.WriteClass(xe, nameof(_coneLimit), _coneLimit);
        xs.WriteClass(xe, nameof(_planesLimit), _planesLimit);
        xs.WriteClass(xe, nameof(_ballSocket), _ballSocket);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpRagdollConstraintDataAtoms);
    }
    public bool Equals(hkpRagdollConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _setupStabilization.Equals(other._setupStabilization) && _ragdollMotors.Equals(other._ragdollMotors) && _angFriction.Equals(other._angFriction) && _twistLimit.Equals(other._twistLimit) && _coneLimit.Equals(other._coneLimit) && _planesLimit.Equals(other._planesLimit) && _ballSocket.Equals(other._ballSocket) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_setupStabilization);
        code.Add(_ragdollMotors);
        code.Add(_angFriction);
        code.Add(_twistLimit);
        code.Add(_coneLimit);
        code.Add(_planesLimit);
        code.Add(_ballSocket);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
