using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkPackfileSectionHeader : IHavokObject, IEquatable<hkPackfileSectionHeader?>
{
    public virtual uint Signature => 0;
    public sbyte[] _sectionTag = new sbyte[19];
    public sbyte _nullByte;
    public int _absoluteDataStart;
    public int _localFixupsOffset;
    public int _globalFixupsOffset;
    public int _virtualFixupsOffset;
    public int _exportsOffset;
    public int _importsOffset;
    public int _endOffset;
    public int[] _pad = new int[4];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _sectionTag = des.ReadSByteCStyleArray(br, 19);
        _nullByte = br.ReadSByte();
        _absoluteDataStart = br.ReadInt32();
        _localFixupsOffset = br.ReadInt32();
        _globalFixupsOffset = br.ReadInt32();
        _virtualFixupsOffset = br.ReadInt32();
        _exportsOffset = br.ReadInt32();
        _importsOffset = br.ReadInt32();
        _endOffset = br.ReadInt32();
        _pad = des.ReadInt32CStyleArray(br, 4);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteSByteCStyleArray(bw, _sectionTag);
        bw.WriteSByte(_nullByte);
        bw.WriteInt32(_absoluteDataStart);
        bw.WriteInt32(_localFixupsOffset);
        bw.WriteInt32(_globalFixupsOffset);
        bw.WriteInt32(_virtualFixupsOffset);
        bw.WriteInt32(_exportsOffset);
        bw.WriteInt32(_importsOffset);
        bw.WriteInt32(_endOffset);
        s.WriteInt32CStyleArray(bw, _pad);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_sectionTag), _sectionTag);
        xs.WriteNumber(xe, nameof(_nullByte), _nullByte);
        xs.WriteNumber(xe, nameof(_absoluteDataStart), _absoluteDataStart);
        xs.WriteNumber(xe, nameof(_localFixupsOffset), _localFixupsOffset);
        xs.WriteNumber(xe, nameof(_globalFixupsOffset), _globalFixupsOffset);
        xs.WriteNumber(xe, nameof(_virtualFixupsOffset), _virtualFixupsOffset);
        xs.WriteNumber(xe, nameof(_exportsOffset), _exportsOffset);
        xs.WriteNumber(xe, nameof(_importsOffset), _importsOffset);
        xs.WriteNumber(xe, nameof(_endOffset), _endOffset);
        xs.WriteNumberArray(xe, nameof(_pad), _pad);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkPackfileSectionHeader);
    }
    public bool Equals(hkPackfileSectionHeader? other)
    {
        return other is not null && _sectionTag.Equals(other._sectionTag) && _nullByte.Equals(other._nullByte) && _absoluteDataStart.Equals(other._absoluteDataStart) && _localFixupsOffset.Equals(other._localFixupsOffset) && _globalFixupsOffset.Equals(other._globalFixupsOffset) && _virtualFixupsOffset.Equals(other._virtualFixupsOffset) && _exportsOffset.Equals(other._exportsOffset) && _importsOffset.Equals(other._importsOffset) && _endOffset.Equals(other._endOffset) && _pad.Equals(other._pad) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_sectionTag);
        code.Add(_nullByte);
        code.Add(_absoluteDataStart);
        code.Add(_localFixupsOffset);
        code.Add(_globalFixupsOffset);
        code.Add(_virtualFixupsOffset);
        code.Add(_exportsOffset);
        code.Add(_importsOffset);
        code.Add(_endOffset);
        code.Add(_pad);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
