using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleFrictionDescription : IHavokObject
{
    public virtual uint Signature => 0;
    public float _wheelDistance;
    public float _chassisMassInv;
    public hknpVehicleFrictionDescriptionAxisDescription?[] _axleDescr = new hknpVehicleFrictionDescriptionAxisDescription?[2];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _wheelDistance = br.ReadSingle();
        _chassisMassInv = br.ReadSingle();
        _axleDescr = des.ReadStructCStyleArray<hknpVehicleFrictionDescriptionAxisDescription>(br, 2);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_wheelDistance);
        bw.WriteSingle(_chassisMassInv);
        s.WriteStructCStyleArray<hknpVehicleFrictionDescriptionAxisDescription>(bw, _axleDescr);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_wheelDistance), _wheelDistance);
        xs.WriteFloat(xe, nameof(_chassisMassInv), _chassisMassInv);
        xs.WriteClassArray(xe, nameof(_axleDescr), _axleDescr);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleFrictionDescription other && _wheelDistance == other._wheelDistance && _chassisMassInv == other._chassisMassInv && _axleDescr == other._axleDescr && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleFrictionDescription? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleFrictionDescription? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelDistance);
        code.Add(_chassisMassInv);
        code.Add(_axleDescr);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
