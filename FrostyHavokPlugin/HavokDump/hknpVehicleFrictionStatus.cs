using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleFrictionStatus : IHavokObject, IEquatable<hknpVehicleFrictionStatus?>
{
    public virtual uint Signature => 0;
    public hknpVehicleFrictionStatusAxisStatus[] _axis = new hknpVehicleFrictionStatusAxisStatus[2];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _axis = des.ReadStructCStyleArray<hknpVehicleFrictionStatusAxisStatus>(br, 2);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStructCStyleArray<hknpVehicleFrictionStatusAxisStatus>(bw, _axis);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray(xe, nameof(_axis), _axis);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleFrictionStatus);
    }
    public bool Equals(hknpVehicleFrictionStatus? other)
    {
        return other is not null && _axis.Equals(other._axis) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_axis);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
