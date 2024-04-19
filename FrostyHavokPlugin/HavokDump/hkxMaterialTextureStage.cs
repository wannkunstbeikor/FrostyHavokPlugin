using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterialTextureStage : IHavokObject
{
    public virtual uint Signature => 0;
    public hkReferencedObject? _texture;
    public hkxMaterial_TextureType _usageHint;
    public int _tcoordChannel;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _texture = des.ReadClassPointer<hkReferencedObject>(br);
        _usageHint = (hkxMaterial_TextureType)br.ReadInt32();
        _tcoordChannel = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassPointer<hkReferencedObject>(bw, _texture);
        bw.WriteInt32((int)_usageHint);
        bw.WriteInt32(_tcoordChannel);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_texture), _texture);
        xs.WriteEnum<hkxMaterial_TextureType, int>(xe, nameof(_usageHint), (int)_usageHint);
        xs.WriteNumber(xe, nameof(_tcoordChannel), _tcoordChannel);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxMaterialTextureStage other && _texture == other._texture && _usageHint == other._usageHint && _tcoordChannel == other._tcoordChannel && Signature == other.Signature;
    }
    public static bool operator ==(hkxMaterialTextureStage? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxMaterialTextureStage? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_texture);
        code.Add(_usageHint);
        code.Add(_tcoordChannel);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
