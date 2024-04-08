using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMaterialLibrary : hkReferencedObject, IEquatable<hknpMaterialLibrary?>
{
    public override uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _materialAddedSignal
    // TYPE_POINTER TYPE_VOID _materialModifiedSignal
    // TYPE_POINTER TYPE_VOID _materialRemovedSignal
    public hkFreeListArrayhknpMaterialhknpMaterialId8hknpMaterialFreeListArrayOperations _entries;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 24; // padding
        _entries = new hkFreeListArrayhknpMaterialhknpMaterialId8hknpMaterialFreeListArrayOperations();
        _entries.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 24; i++) bw.WriteByte(0); // padding
        _entries.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_entries), _entries);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpMaterialLibrary);
    }
    public bool Equals(hknpMaterialLibrary? other)
    {
        return other is not null && _entries.Equals(other._entries) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_entries);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
