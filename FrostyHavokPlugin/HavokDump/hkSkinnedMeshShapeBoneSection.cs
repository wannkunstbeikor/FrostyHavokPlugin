using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSkinnedMeshShapeBoneSection : IHavokObject
{
    public virtual uint Signature => 0;
    public hkMeshShape? _meshBuffer;
    public ushort _startBoneSetId;
    public short _numBoneSets;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _meshBuffer = des.ReadClassPointer<hkMeshShape>(br);
        _startBoneSetId = br.ReadUInt16();
        _numBoneSets = br.ReadInt16();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassPointer<hkMeshShape>(bw, _meshBuffer);
        bw.WriteUInt16(_startBoneSetId);
        bw.WriteInt16(_numBoneSets);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_meshBuffer), _meshBuffer);
        xs.WriteNumber(xe, nameof(_startBoneSetId), _startBoneSetId);
        xs.WriteNumber(xe, nameof(_numBoneSets), _numBoneSets);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkSkinnedMeshShapeBoneSection other && _meshBuffer == other._meshBuffer && _startBoneSetId == other._startBoneSetId && _numBoneSets == other._numBoneSets && Signature == other.Signature;
    }
    public static bool operator ==(hkSkinnedMeshShapeBoneSection? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkSkinnedMeshShapeBoneSection? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_meshBuffer);
        code.Add(_startBoneSetId);
        code.Add(_numBoneSets);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
