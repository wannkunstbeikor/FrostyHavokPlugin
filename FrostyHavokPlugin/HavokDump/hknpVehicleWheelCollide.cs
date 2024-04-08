using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleWheelCollide : hkReferencedObject, IEquatable<hknpVehicleWheelCollide?>
{
    public override uint Signature => 0;
    public bool _alreadyUsed;
    // TYPE_ENUM TYPE_UINT8 _type
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _alreadyUsed = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteBoolean(_alreadyUsed);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_alreadyUsed), _alreadyUsed);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleWheelCollide);
    }
    public bool Equals(hknpVehicleWheelCollide? other)
    {
        return other is not null && _alreadyUsed.Equals(other._alreadyUsed) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_alreadyUsed);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
