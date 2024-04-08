using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSkinnedMeshShapeBoneSet : IHavokObject, IEquatable<hkSkinnedMeshShapeBoneSet?>
{
    public virtual uint Signature => 0;
    public ushort _boneBufferOffset;
    public ushort _numBones;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _boneBufferOffset = br.ReadUInt16();
        _numBones = br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt16(_boneBufferOffset);
        bw.WriteUInt16(_numBones);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_boneBufferOffset), _boneBufferOffset);
        xs.WriteNumber(xe, nameof(_numBones), _numBones);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSkinnedMeshShapeBoneSet);
    }
    public bool Equals(hkSkinnedMeshShapeBoneSet? other)
    {
        return other is not null && _boneBufferOffset.Equals(other._boneBufferOffset) && _numBones.Equals(other._numBones) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_boneBufferOffset);
        code.Add(_numBones);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
