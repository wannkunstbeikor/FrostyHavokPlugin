using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters : IHavokObject, IEquatable<hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters?>
{
    public virtual uint Signature => 0;
    public float _strength;
    public float _dampingCompression;
    public float _dampingRelaxation;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _strength = br.ReadSingle();
        _dampingCompression = br.ReadSingle();
        _dampingRelaxation = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_strength);
        bw.WriteSingle(_dampingCompression);
        bw.WriteSingle(_dampingRelaxation);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_strength), _strength);
        xs.WriteFloat(xe, nameof(_dampingCompression), _dampingCompression);
        xs.WriteFloat(xe, nameof(_dampingRelaxation), _dampingRelaxation);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters);
    }
    public bool Equals(hknpVehicleDefaultSuspensionWheelSpringSuspensionParameters? other)
    {
        return other is not null && _strength.Equals(other._strength) && _dampingCompression.Equals(other._dampingCompression) && _dampingRelaxation.Equals(other._dampingRelaxation) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_strength);
        code.Add(_dampingCompression);
        code.Add(_dampingRelaxation);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
