using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMalleableConstraintData : hkpWrappedConstraintData
{
    public override uint Signature => 0;
    public hknpBridgeConstraintAtom? _atom;
    public bool _wantsRuntime;
    public float _strength;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _atom = new hknpBridgeConstraintAtom();
        _atom.Read(des, br);
        _wantsRuntime = br.ReadBoolean();
        br.Position += 3; // padding
        _strength = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _atom.Write(s, bw);
        bw.WriteBoolean(_wantsRuntime);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_strength);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_atom), _atom);
        xs.WriteBoolean(xe, nameof(_wantsRuntime), _wantsRuntime);
        xs.WriteFloat(xe, nameof(_strength), _strength);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpMalleableConstraintData other && base.Equals(other) && _atom == other._atom && _wantsRuntime == other._wantsRuntime && _strength == other._strength && Signature == other.Signature;
    }
    public static bool operator ==(hknpMalleableConstraintData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMalleableConstraintData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_atom);
        code.Add(_wantsRuntime);
        code.Add(_strength);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
