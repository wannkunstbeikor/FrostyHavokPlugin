using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMotionPropertiesLibrary : hkReferencedObject
{
    public override uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _entryAddedSignal
    // TYPE_POINTER TYPE_VOID _entryModifiedSignal
    // TYPE_POINTER TYPE_VOID _entryRemovedSignal
    public hkFreeListArrayhknpMotionPropertieshknpMotionPropertiesId8hknpMotionPropertiesFreeListArrayOperations? _entries;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 24; // padding
        _entries = new hkFreeListArrayhknpMotionPropertieshknpMotionPropertiesId8hknpMotionPropertiesFreeListArrayOperations();
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
        return obj is hknpMotionPropertiesLibrary other && base.Equals(other) && _entries == other._entries && Signature == other.Signature;
    }
    public static bool operator ==(hknpMotionPropertiesLibrary? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMotionPropertiesLibrary? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_entries);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
