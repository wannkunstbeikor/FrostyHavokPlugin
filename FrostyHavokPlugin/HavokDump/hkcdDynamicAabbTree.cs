using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicAabbTree : hkReferencedObject, IEquatable<hkcdDynamicAabbTree?>
{
    public override uint Signature => 0;
    // TYPE_BOOL TYPE_VOID _shouldDeleteTree
    public hkcdDynamicTreeDefaultTree48Storage _treePtr;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 8; // padding
        _treePtr = des.ReadClassPointer<hkcdDynamicTreeDefaultTree48Storage>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hkcdDynamicTreeDefaultTree48Storage>(bw, _treePtr);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_treePtr), _treePtr);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicAabbTree);
    }
    public bool Equals(hkcdDynamicAabbTree? other)
    {
        return other is not null && _treePtr.Equals(other._treePtr) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_treePtr);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
