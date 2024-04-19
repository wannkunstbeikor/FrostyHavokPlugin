using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkGizmoAttribute : IHavokObject
{
    public virtual uint Signature => 0;
    public bool _visible;
    public string _label = string.Empty;
    public hkGizmoAttribute_GizmoType _type;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _visible = br.ReadBoolean();
        br.Position += 7; // padding
        _label = des.ReadStringPointer(br);
        _type = (hkGizmoAttribute_GizmoType)br.ReadSByte();
        br.Position += 7; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteBoolean(_visible);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
        s.WriteStringPointer(bw, _label);
        bw.WriteSByte((sbyte)_type);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteBoolean(xe, nameof(_visible), _visible);
        xs.WriteString(xe, nameof(_label), _label);
        xs.WriteEnum<hkGizmoAttribute_GizmoType, sbyte>(xe, nameof(_type), (sbyte)_type);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkGizmoAttribute other && _visible == other._visible && _label == other._label && _type == other._type && Signature == other.Signature;
    }
    public static bool operator ==(hkGizmoAttribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkGizmoAttribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_visible);
        code.Add(_label);
        code.Add(_type);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
