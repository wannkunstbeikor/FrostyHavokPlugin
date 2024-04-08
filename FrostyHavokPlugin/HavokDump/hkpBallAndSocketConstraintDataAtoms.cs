using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpBallAndSocketConstraintDataAtoms : IHavokObject, IEquatable<hkpBallAndSocketConstraintDataAtoms?>
{
    public virtual uint Signature => 0;
    public hkpSetLocalTranslationsConstraintAtom _pivots;
    public hkpSetupStabilizationAtom _setupStabilization;
    public hkpBallSocketConstraintAtom _ballSocket;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _pivots = new hkpSetLocalTranslationsConstraintAtom();
        _pivots.Read(des, br);
        _setupStabilization = new hkpSetupStabilizationAtom();
        _setupStabilization.Read(des, br);
        _ballSocket = new hkpBallSocketConstraintAtom();
        _ballSocket.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _pivots.Write(s, bw);
        _setupStabilization.Write(s, bw);
        _ballSocket.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_pivots), _pivots);
        xs.WriteClass(xe, nameof(_setupStabilization), _setupStabilization);
        xs.WriteClass(xe, nameof(_ballSocket), _ballSocket);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpBallAndSocketConstraintDataAtoms);
    }
    public bool Equals(hkpBallAndSocketConstraintDataAtoms? other)
    {
        return other is not null && _pivots.Equals(other._pivots) && _setupStabilization.Equals(other._setupStabilization) && _ballSocket.Equals(other._ballSocket) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_pivots);
        code.Add(_setupStabilization);
        code.Add(_ballSocket);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
