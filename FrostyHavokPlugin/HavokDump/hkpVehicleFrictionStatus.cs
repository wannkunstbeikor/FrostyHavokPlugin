using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpVehicleFrictionStatus : IHavokObject, IEquatable<hkpVehicleFrictionStatus?>
{
    public virtual uint Signature => 0;
    public hkpVehicleFrictionStatusAxisStatus[] _axis = new hkpVehicleFrictionStatusAxisStatus[2];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _axis = des.ReadStructCStyleArray<hkpVehicleFrictionStatusAxisStatus>(br, 2);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStructCStyleArray<hkpVehicleFrictionStatusAxisStatus>(bw, _axis);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray(xe, nameof(_axis), _axis);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpVehicleFrictionStatus);
    }
    public bool Equals(hkpVehicleFrictionStatus? other)
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
