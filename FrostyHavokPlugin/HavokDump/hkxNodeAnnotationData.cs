using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxNodeAnnotationData : IHavokObject
{
    public virtual uint Signature => 0;
    public float _time;
    public string _description = string.Empty;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _time = br.ReadSingle();
        br.Position += 4; // padding
        _description = des.ReadStringPointer(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_time);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteStringPointer(bw, _description);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_time), _time);
        xs.WriteString(xe, nameof(_description), _description);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxNodeAnnotationData other && _time == other._time && _description == other._description && Signature == other.Signature;
    }
    public static bool operator ==(hkxNodeAnnotationData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxNodeAnnotationData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_time);
        code.Add(_description);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
