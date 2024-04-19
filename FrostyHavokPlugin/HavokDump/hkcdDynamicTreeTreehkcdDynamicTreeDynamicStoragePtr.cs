using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeTreehkcdDynamicTreeDynamicStoragePtr : hkcdDynamicTreeDynamicStoragePtr
{
    public override uint Signature => 0;
    public uint _numLeaves;
    public uint _path;
    public ulong _root;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _numLeaves = br.ReadUInt32();
        _path = br.ReadUInt32();
        _root = br.ReadUInt64();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_numLeaves);
        bw.WriteUInt32(_path);
        bw.WriteUInt64(_root);
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
        return obj is hkcdDynamicTreeTreehkcdDynamicTreeDynamicStoragePtr other && base.Equals(other) && _numLeaves == other._numLeaves && _path == other._path && _root == other._root && Signature == other.Signature;
    }
    public static bool operator ==(hkcdDynamicTreeTreehkcdDynamicTreeDynamicStoragePtr? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdDynamicTreeTreehkcdDynamicTreeDynamicStoragePtr? a, object? b) => !(a == b);
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
