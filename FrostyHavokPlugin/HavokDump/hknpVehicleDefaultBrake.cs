using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultBrake : hknpVehicleBrake, IEquatable<hknpVehicleDefaultBrake?>
{
    public override uint Signature => 0;
    public List<hknpVehicleDefaultBrakeWheelBrakingProperties> _wheelBrakingProperties;
    public float _wheelsMinTimeToBlock;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _wheelBrakingProperties = des.ReadClassArray<hknpVehicleDefaultBrakeWheelBrakingProperties>(br);
        _wheelsMinTimeToBlock = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hknpVehicleDefaultBrakeWheelBrakingProperties>(bw, _wheelBrakingProperties);
        bw.WriteSingle(_wheelsMinTimeToBlock);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hknpVehicleDefaultBrakeWheelBrakingProperties>(xe, nameof(_wheelBrakingProperties), _wheelBrakingProperties);
        xs.WriteFloat(xe, nameof(_wheelsMinTimeToBlock), _wheelsMinTimeToBlock);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultBrake);
    }
    public bool Equals(hknpVehicleDefaultBrake? other)
    {
        return other is not null && _wheelBrakingProperties.Equals(other._wheelBrakingProperties) && _wheelsMinTimeToBlock.Equals(other._wheelsMinTimeToBlock) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_wheelBrakingProperties);
        code.Add(_wheelsMinTimeToBlock);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
