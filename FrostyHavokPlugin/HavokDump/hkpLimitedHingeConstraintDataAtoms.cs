using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLimitedHingeConstraintDataAtoms : IHavokObject, IEquatable<hkpLimitedHingeConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom _transforms;
    public hkpSetupStabilizationAtom _setupStabilization;
    public hkpAngMotorConstraintAtom _angMotor;
    public hkpAngFrictionConstraintAtom _angFriction;
    public hkpAngLimitConstraintAtom _angLimit;
    public hkp2dAngConstraintAtom _2dAng;
    public hkpBallSocketConstraintAtom _ballSocket;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _setupStabilization = new hkpSetupStabilizationAtom();
        _setupStabilization.Read(des, br);
        _angMotor = new hkpAngMotorConstraintAtom();
        _angMotor.Read(des, br);
        _angFriction = new hkpAngFrictionConstraintAtom();
        _angFriction.Read(des, br);
        _angLimit = new hkpAngLimitConstraintAtom();
        _angLimit.Read(des, br);
        _2dAng = new hkp2dAngConstraintAtom();
        _2dAng.Read(des, br);
        _ballSocket = new hkpBallSocketConstraintAtom();
        _ballSocket.Read(des, br);
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _setupStabilization.Write(s, bw);
        _angMotor.Write(s, bw);
        _angFriction.Write(s, bw);
        _angLimit.Write(s, bw);
        _2dAng.Write(s, bw);
        _ballSocket.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_setupStabilization), _setupStabilization);
        xs.WriteClass(xe, nameof(_angMotor), _angMotor);
        xs.WriteClass(xe, nameof(_angFriction), _angFriction);
        xs.WriteClass(xe, nameof(_angLimit), _angLimit);
        xs.WriteClass(xe, nameof(_2dAng), _2dAng);
        xs.WriteClass(xe, nameof(_ballSocket), _ballSocket);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpLimitedHingeConstraintDataAtoms);
    }
    public bool Equals(hkpLimitedHingeConstraintDataAtoms? other)
    {
        return other is not null && _transforms.Equals(other._transforms) && _setupStabilization.Equals(other._setupStabilization) && _angMotor.Equals(other._angMotor) && _angFriction.Equals(other._angFriction) && _angLimit.Equals(other._angLimit) && _2dAng.Equals(other._2dAng) && _ballSocket.Equals(other._ballSocket) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_setupStabilization);
        code.Add(_angMotor);
        code.Add(_angFriction);
        code.Add(_angLimit);
        code.Add(_2dAng);
        code.Add(_ballSocket);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
