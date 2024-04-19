using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexBuffer : hkReferencedObject
{
    public override uint Signature => 0;
    public hkxVertexBufferVertexData? _data;
    public hkxVertexDescription? _desc;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _data = new hkxVertexBufferVertexData();
        _data.Read(des, br);
        _desc = new hkxVertexDescription();
        _desc.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _data.Write(s, bw);
        _desc.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_data), _data);
        xs.WriteClass(xe, nameof(_desc), _desc);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxVertexBuffer other && base.Equals(other) && _data == other._data && _desc == other._desc && Signature == other.Signature;
    }
    public static bool operator ==(hkxVertexBuffer? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxVertexBuffer? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(_desc);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
