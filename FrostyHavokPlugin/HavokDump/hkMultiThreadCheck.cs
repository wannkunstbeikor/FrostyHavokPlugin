using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMultiThreadCheck : IHavokObject, IEquatable<hkMultiThreadCheck?>
{
    public virtual uint Signature => 0;
    // TYPE_UINT32 TYPE_VOID _threadId
    // TYPE_INT32 TYPE_VOID _stackTraceId
    // TYPE_UINT16 TYPE_VOID _markCount
    // TYPE_UINT16 TYPE_VOID _markBitStack
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        br.Position += 12; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMultiThreadCheck);
    }
    public bool Equals(hkMultiThreadCheck? other)
    {
        return other is not null && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}