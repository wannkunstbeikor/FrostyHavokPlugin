using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpSetupStabilizationAtom : hkpConstraintAtom, IEquatable<hkpSetupStabilizationAtom?>
{
    public override uint Signature => 0;
    public bool _enabled;
    public float _maxLinImpulse;
    public float _maxAngImpulse;
    public float _maxAngle;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _enabled = br.ReadBoolean();
        br.Position += 1; // padding
        _maxLinImpulse = br.ReadSingle();
        _maxAngImpulse = br.ReadSingle();
        _maxAngle = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteBoolean(_enabled);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_maxLinImpulse);
        bw.WriteSingle(_maxAngImpulse);
        bw.WriteSingle(_maxAngle);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_enabled), _enabled);
        xs.WriteFloat(xe, nameof(_maxLinImpulse), _maxLinImpulse);
        xs.WriteFloat(xe, nameof(_maxAngImpulse), _maxAngImpulse);
        xs.WriteFloat(xe, nameof(_maxAngle), _maxAngle);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpSetupStabilizationAtom);
    }
    public bool Equals(hkpSetupStabilizationAtom? other)
    {
        return other is not null && _enabled.Equals(other._enabled) && _maxLinImpulse.Equals(other._maxLinImpulse) && _maxAngImpulse.Equals(other._maxAngImpulse) && _maxAngle.Equals(other._maxAngle) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_enabled);
        code.Add(_maxLinImpulse);
        code.Add(_maxAngImpulse);
        code.Add(_maxAngle);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
