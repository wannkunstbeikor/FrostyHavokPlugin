using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultSuspension : hknpVehicleSuspension, IEquatable<hknpVehicleDefaultSuspension?>
{
    public override uint Signature => 0;
    public List<hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters> _wheelSpringParams;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelSpringParams = des.ReadClassArray<hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters>(bw, _wheelSpringParams);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters>(xe, nameof(_wheelSpringParams), _wheelSpringParams);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultSuspension);
    }
    public bool Equals(hknpVehicleDefaultSuspension? other)
    {
        return other is not null && _wheelSpringParams.Equals(other._wheelSpringParams) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelSpringParams);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
