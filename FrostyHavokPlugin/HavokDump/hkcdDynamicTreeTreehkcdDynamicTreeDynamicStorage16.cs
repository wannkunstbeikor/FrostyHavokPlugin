using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorage16 : hkcdDynamicTreeDynamicStorage16
{
    public override uint Signature => 0;
    public uint _numLeaves;
    public uint _path;
    public ushort _root;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _numLeaves = br.ReadUInt32();
        _path = br.ReadUInt32();
        _root = br.ReadUInt16();
        br.Position += 6; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_numLeaves);
        bw.WriteUInt32(_path);
        bw.WriteUInt16(_root);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
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
        return obj is hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorage16 other && base.Equals(other) && _numLeaves == other._numLeaves && _path == other._path && _root == other._root && Signature == other.Signature;
    }
    public static bool operator ==(hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorage16? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdDynamicTreeTreehkcdDynamicTreeDynamicStorage16? a, object? b) => !(a == b);
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
