using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkUiAttribute : IHavokObject
{
    public virtual uint Signature => 0;
    public bool _visible;
    public bool _editable;
    public hkUiAttribute_HideInModeler _hideInModeler;
    public string _label = string.Empty;
    public string _group = string.Empty;
    public string _hideBaseClassMembers = string.Empty;
    public bool _endGroup;
    public bool _endGroup2;
    public bool _advanced;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _visible = br.ReadBoolean();
        _editable = br.ReadBoolean();
        _hideInModeler = (hkUiAttribute_HideInModeler)br.ReadSByte();
        br.Position += 5; // padding
        _label = des.ReadStringPointer(br);
        _group = des.ReadStringPointer(br);
        _hideBaseClassMembers = des.ReadStringPointer(br);
        _endGroup = br.ReadBoolean();
        _endGroup2 = br.ReadBoolean();
        _advanced = br.ReadBoolean();
        br.Position += 5; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteBoolean(_visible);
        bw.WriteBoolean(_editable);
        bw.WriteSByte((sbyte)_hideInModeler);
        for (int i = 0; i < 5; i++) bw.WriteByte(0); // padding
        s.WriteStringPointer(bw, _label);
        s.WriteStringPointer(bw, _group);
        s.WriteStringPointer(bw, _hideBaseClassMembers);
        bw.WriteBoolean(_endGroup);
        bw.WriteBoolean(_endGroup2);
        bw.WriteBoolean(_advanced);
        for (int i = 0; i < 5; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteBoolean(xe, nameof(_visible), _visible);
        xs.WriteBoolean(xe, nameof(_editable), _editable);
        xs.WriteEnum<hkUiAttribute_HideInModeler, sbyte>(xe, nameof(_hideInModeler), (sbyte)_hideInModeler);
        xs.WriteString(xe, nameof(_label), _label);
        xs.WriteString(xe, nameof(_group), _group);
        xs.WriteString(xe, nameof(_hideBaseClassMembers), _hideBaseClassMembers);
        xs.WriteBoolean(xe, nameof(_endGroup), _endGroup);
        xs.WriteBoolean(xe, nameof(_endGroup2), _endGroup2);
        xs.WriteBoolean(xe, nameof(_advanced), _advanced);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkUiAttribute other && _visible == other._visible && _editable == other._editable && _hideInModeler == other._hideInModeler && _label == other._label && _group == other._group && _hideBaseClassMembers == other._hideBaseClassMembers && _endGroup == other._endGroup && _endGroup2 == other._endGroup2 && _advanced == other._advanced && Signature == other.Signature;
    }
    public static bool operator ==(hkUiAttribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkUiAttribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_visible);
        code.Add(_editable);
        code.Add(_hideInModeler);
        code.Add(_label);
        code.Add(_group);
        code.Add(_hideBaseClassMembers);
        code.Add(_endGroup);
        code.Add(_endGroup2);
        code.Add(_advanced);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
