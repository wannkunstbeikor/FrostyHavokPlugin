using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpTyremarksInfo : hkReferencedObject
{
    public override uint Signature => 0;
    public float _minTyremarkEnergy;
    public float _maxTyremarkEnergy;
    public List<hknpTyremarksWheel?> _tyremarksWheel = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _minTyremarkEnergy = br.ReadSingle();
        _maxTyremarkEnergy = br.ReadSingle();
        _tyremarksWheel = des.ReadClassPointerArray<hknpTyremarksWheel>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_minTyremarkEnergy);
        bw.WriteSingle(_maxTyremarkEnergy);
        s.WriteClassPointerArray<hknpTyremarksWheel>(bw, _tyremarksWheel);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_minTyremarkEnergy), _minTyremarkEnergy);
        xs.WriteFloat(xe, nameof(_maxTyremarkEnergy), _maxTyremarkEnergy);
        xs.WriteClassPointerArray<hknpTyremarksWheel>(xe, nameof(_tyremarksWheel), _tyremarksWheel);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpTyremarksInfo other && base.Equals(other) && _minTyremarkEnergy == other._minTyremarkEnergy && _maxTyremarkEnergy == other._maxTyremarkEnergy && _tyremarksWheel.SequenceEqual(other._tyremarksWheel) && Signature == other.Signature;
    }
    public static bool operator ==(hknpTyremarksInfo? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpTyremarksInfo? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_minTyremarkEnergy);
        code.Add(_maxTyremarkEnergy);
        code.Add(_tyremarksWheel);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
