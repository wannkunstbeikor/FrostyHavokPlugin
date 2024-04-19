using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMeshSection : hkReferencedObject
{
    public override uint Signature => 0;
    public hkxVertexBuffer? _vertexBuffer;
    public List<hkxIndexBuffer?> _indexBuffers = new();
    public hkxMaterial? _material;
    public List<hkReferencedObject?> _userChannels = new();
    public List<hkxVertexAnimation?> _vertexAnimations = new();
    public List<float> _linearKeyFrameHints = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _vertexBuffer = des.ReadClassPointer<hkxVertexBuffer>(br);
        _indexBuffers = des.ReadClassPointerArray<hkxIndexBuffer>(br);
        _material = des.ReadClassPointer<hkxMaterial>(br);
        _userChannels = des.ReadClassPointerArray<hkReferencedObject>(br);
        _vertexAnimations = des.ReadClassPointerArray<hkxVertexAnimation>(br);
        _linearKeyFrameHints = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkxVertexBuffer>(bw, _vertexBuffer);
        s.WriteClassPointerArray<hkxIndexBuffer>(bw, _indexBuffers);
        s.WriteClassPointer<hkxMaterial>(bw, _material);
        s.WriteClassPointerArray<hkReferencedObject>(bw, _userChannels);
        s.WriteClassPointerArray<hkxVertexAnimation>(bw, _vertexAnimations);
        s.WriteSingleArray(bw, _linearKeyFrameHints);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_vertexBuffer), _vertexBuffer);
        xs.WriteClassPointerArray<hkxIndexBuffer>(xe, nameof(_indexBuffers), _indexBuffers);
        xs.WriteClassPointer(xe, nameof(_material), _material);
        xs.WriteClassPointerArray<hkReferencedObject>(xe, nameof(_userChannels), _userChannels);
        xs.WriteClassPointerArray<hkxVertexAnimation>(xe, nameof(_vertexAnimations), _vertexAnimations);
        xs.WriteFloatArray(xe, nameof(_linearKeyFrameHints), _linearKeyFrameHints);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxMeshSection other && base.Equals(other) && _vertexBuffer == other._vertexBuffer && _indexBuffers.SequenceEqual(other._indexBuffers) && _material == other._material && _userChannels.SequenceEqual(other._userChannels) && _vertexAnimations.SequenceEqual(other._vertexAnimations) && _linearKeyFrameHints.SequenceEqual(other._linearKeyFrameHints) && Signature == other.Signature;
    }
    public static bool operator ==(hkxMeshSection? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxMeshSection? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertexBuffer);
        code.Add(_indexBuffers);
        code.Add(_material);
        code.Add(_userChannels);
        code.Add(_vertexAnimations);
        code.Add(_linearKeyFrameHints);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
