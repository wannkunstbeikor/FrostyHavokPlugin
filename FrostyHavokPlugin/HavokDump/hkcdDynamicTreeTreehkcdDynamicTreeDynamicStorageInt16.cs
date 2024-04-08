using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorageInt16 : hkcdDynamicTreeDynamicStorageInt16, IEquatable<hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorageInt16?>
{
    public override uint Signature => 0;
    public uint _numLeaves;
    public uint _path;
    public uint _root;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _numLeaves = br.ReadUInt32();
        _path = br.ReadUInt32();
        _root = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_numLeaves);
        bw.WriteUInt32(_path);
        bw.WriteUInt32(_root);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_numLeaves), _numLeaves);
        xs.WriteNumber(xe, nameof(_path), _path);
        xs.WriteNumber(xe, nameof(_root), _root);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorageInt16);
    }
    public bool Equals(hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorageInt16? other)
    {
        return other is not null && _numLeaves.Equals(other._numLeaves) && _path.Equals(other._path) && _root.Equals(other._root) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_numLeaves);
        code.Add(_path);
        code.Add(_root);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
