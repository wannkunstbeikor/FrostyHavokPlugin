using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshBody : hkMeshBody
{
    public override uint Signature => 0;
    public Matrix4 _transform;
    public hkIndexedTransformSet? _transformSet;
    public hkMeshShape? _shape;
    public List<hkMeshVertexBuffer?> _vertexBuffers = new();
    public string _name = string.Empty;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _transform = des.ReadMatrix4(br);
        _transformSet = des.ReadClassPointer<hkIndexedTransformSet>(br);
        _shape = des.ReadClassPointer<hkMeshShape>(br);
        _vertexBuffers = des.ReadClassPointerArray<hkMeshVertexBuffer>(br);
        _name = des.ReadStringPointer(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteMatrix4(bw, _transform);
        s.WriteClassPointer<hkIndexedTransformSet>(bw, _transformSet);
        s.WriteClassPointer<hkMeshShape>(bw, _shape);
        s.WriteClassPointerArray<hkMeshVertexBuffer>(bw, _vertexBuffers);
        s.WriteStringPointer(bw, _name);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteMatrix4(xe, nameof(_transform), _transform);
        xs.WriteClassPointer(xe, nameof(_transformSet), _transformSet);
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteClassPointerArray<hkMeshVertexBuffer>(xe, nameof(_vertexBuffers), _vertexBuffers);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMemoryMeshBody other && base.Equals(other) && _transform == other._transform && _transformSet == other._transformSet && _shape == other._shape && _vertexBuffers.SequenceEqual(other._vertexBuffers) && _name == other._name && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryMeshBody? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryMeshBody? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transform);
        code.Add(_transformSet);
        code.Add(_shape);
        code.Add(_vertexBuffers);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
