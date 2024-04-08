using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexAnimation : hkReferencedObject, IEquatable<hkxVertexAnimation?>
{
    public override uint Signature => 0;
    public float _time;
    public hkxVertexBuffer _vertData;
    public List<int> _vertexIndexMap;
    public List<hkxVertexAnimationUsageMap> _componentMap;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _time = br.ReadSingle();
        br.Position += 4; // padding
        _vertData = new hkxVertexBuffer();
        _vertData.Read(des, br);
        _vertexIndexMap = des.ReadInt32Array(br);
        _componentMap = des.ReadClassArray<hkxVertexAnimationUsageMap>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_time);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        _vertData.Write(s, bw);
        s.WriteInt32Array(bw, _vertexIndexMap);
        s.WriteClassArray<hkxVertexAnimationUsageMap>(bw, _componentMap);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_time), _time);
        xs.WriteClass(xe, nameof(_vertData), _vertData);
        xs.WriteNumberArray(xe, nameof(_vertexIndexMap), _vertexIndexMap);
        xs.WriteClassArray<hkxVertexAnimationUsageMap>(xe, nameof(_componentMap), _componentMap);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxVertexAnimation);
    }
    public bool Equals(hkxVertexAnimation? other)
    {
        return other is not null && _time.Equals(other._time) && _vertData.Equals(other._vertData) && _vertexIndexMap.Equals(other._vertexIndexMap) && _componentMap.Equals(other._componentMap) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_time);
        code.Add(_vertData);
        code.Add(_vertexIndexMap);
        code.Add(_componentMap);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
