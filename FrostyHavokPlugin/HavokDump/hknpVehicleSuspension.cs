using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleSuspension : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hknpVehicleSuspensionSuspensionWheelParameters?> _wheelParams = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelParams = des.ReadClassArray<hknpVehicleSuspensionSuspensionWheelParameters>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hknpVehicleSuspensionSuspensionWheelParameters>(bw, _wheelParams);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hknpVehicleSuspensionSuspensionWheelParameters>(xe, nameof(_wheelParams), _wheelParams);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpVehicleSuspension other && base.Equals(other) && _wheelParams.SequenceEqual(other._wheelParams) && Signature == other.Signature;
    }
    public static bool operator ==(hknpVehicleSuspension? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpVehicleSuspension? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelParams);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
