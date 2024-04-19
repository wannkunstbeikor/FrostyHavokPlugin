using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryResourceHandleExternalLink : IHavokObject
{
    public virtual uint Signature => 0;
    public string _memberName = string.Empty;
    public string _externalId = string.Empty;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _memberName = des.ReadStringPointer(br);
        _externalId = des.ReadStringPointer(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _memberName);
        s.WriteStringPointer(bw, _externalId);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_memberName), _memberName);
        xs.WriteString(xe, nameof(_externalId), _externalId);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMemoryResourceHandleExternalLink other && _memberName == other._memberName && _externalId == other._externalId && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryResourceHandleExternalLink? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryResourceHandleExternalLink? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_memberName);
        code.Add(_externalId);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
