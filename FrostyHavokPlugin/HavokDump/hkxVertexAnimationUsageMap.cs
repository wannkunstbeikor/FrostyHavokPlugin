using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexAnimationUsageMap : IHavokObject, IEquatable<hkxVertexAnimationUsageMap?>
{
    public virtual uint Signature => 0;
    public hkxVertexDescription_DataUsage _use;
    public byte _useIndexOrig;
    public byte _useIndexLocal;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _use = (hkxVertexDescription_DataUsage)br.ReadUInt16();
        _useIndexOrig = br.ReadByte();
        _useIndexLocal = br.ReadByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt16((ushort)_use);
        bw.WriteByte(_useIndexOrig);
        bw.WriteByte(_useIndexLocal);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteEnum<hkxVertexDescription_DataUsage, ushort>(xe, nameof(_use), (ushort)_use);
        xs.WriteNumber(xe, nameof(_useIndexOrig), _useIndexOrig);
        xs.WriteNumber(xe, nameof(_useIndexLocal), _useIndexLocal);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxVertexAnimationUsageMap);
    }
    public bool Equals(hkxVertexAnimationUsageMap? other)
    {
        return other is not null && _use.Equals(other._use) && _useIndexOrig.Equals(other._useIndexOrig) && _useIndexLocal.Equals(other._useIndexLocal) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_use);
        code.Add(_useIndexOrig);
        code.Add(_useIndexLocal);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
