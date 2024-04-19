using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpFixedConstraintDataAtoms : IHavokObject
{
    public virtual uint Signature => 0;
    public hkpSetLocalTransformsConstraintAtom? _transforms;
    public hkpSetupStabilizationAtom? _setupStabilization;
    public hkpBallSocketConstraintAtom? _ballSocket;
    public hkp3dAngConstraintAtom? _ang;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transforms = new hkpSetLocalTransformsConstraintAtom();
        _transforms.Read(des, br);
        _setupStabilization = new hkpSetupStabilizationAtom();
        _setupStabilization.Read(des, br);
        _ballSocket = new hkpBallSocketConstraintAtom();
        _ballSocket.Read(des, br);
        _ang = new hkp3dAngConstraintAtom();
        _ang.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _transforms.Write(s, bw);
        _setupStabilization.Write(s, bw);
        _ballSocket.Write(s, bw);
        _ang.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_transforms), _transforms);
        xs.WriteClass(xe, nameof(_setupStabilization), _setupStabilization);
        xs.WriteClass(xe, nameof(_ballSocket), _ballSocket);
        xs.WriteClass(xe, nameof(_ang), _ang);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpFixedConstraintDataAtoms other && _transforms == other._transforms && _setupStabilization == other._setupStabilization && _ballSocket == other._ballSocket && _ang == other._ang && Signature == other.Signature;
    }
    public static bool operator ==(hkpFixedConstraintDataAtoms? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpFixedConstraintDataAtoms? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transforms);
        code.Add(_setupStabilization);
        code.Add(_ballSocket);
        code.Add(_ang);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
