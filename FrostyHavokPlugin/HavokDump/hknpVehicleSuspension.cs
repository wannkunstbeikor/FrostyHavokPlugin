using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleSuspension : hkReferencedObject, IEquatable<hknpVehicleSuspension?>
{
    public override uint Signature => 0;
    public List<hknpVehicleSuspensionSuspensionWheelParameters> _wheelParams;
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
        return Equals(obj as hknpVehicleSuspension);
    }
    public bool Equals(hknpVehicleSuspension? other)
    {
        return other is not null && _wheelParams.Equals(other._wheelParams) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelParams);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
