using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpTyremarksInfo : hkReferencedObject, IEquatable<hknpTyremarksInfo?>
{
    public override uint Signature => 0;
    public float _minTyremarkEnergy;
    public float _maxTyremarkEnergy;
    public List<hknpTyremarksWheel> _tyremarksWheel;
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
        return Equals(obj as hknpTyremarksInfo);
    }
    public bool Equals(hknpTyremarksInfo? other)
    {
        return other is not null && _minTyremarkEnergy.Equals(other._minTyremarkEnergy) && _maxTyremarkEnergy.Equals(other._maxTyremarkEnergy) && _tyremarksWheel.Equals(other._tyremarksWheel) && Signature == other.Signature;
    }
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
