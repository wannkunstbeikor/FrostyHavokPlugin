using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterialShaderSet : hkReferencedObject, IEquatable<hkxMaterialShaderSet?>
{
    public override uint Signature => 0;
    public List<hkxMaterialShader> _shaders;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _shaders = des.ReadClassPointerArray<hkxMaterialShader>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointerArray<hkxMaterialShader>(bw, _shaders);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointerArray<hkxMaterialShader>(xe, nameof(_shaders), _shaders);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxMaterialShaderSet);
    }
    public bool Equals(hkxMaterialShaderSet? other)
    {
        return other is not null && _shaders.Equals(other._shaders) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_shaders);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
