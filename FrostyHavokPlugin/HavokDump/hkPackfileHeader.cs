using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkPackfileHeader : IHavokObject, IEquatable<hkPackfileHeader?>
{
    public virtual uint Signature => 0;
    public int[] _magic = new int[2];
    public int _userTag;
    public int _fileVersion;
    public byte[] _layoutRules = new byte[4];
    public int _numSections;
    public int _contentsSectionIndex;
    public int _contentsSectionOffset;
    public int _contentsClassNameSectionIndex;
    public int _contentsClassNameSectionOffset;
    public sbyte[] _contentsVersion = new sbyte[16];
    public int _flags;
    public ushort _maxpredicate;
    public ushort _predicateArraySizePlusPadding;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _magic = des.ReadInt32CStyleArray(br, 2);
        _userTag = br.ReadInt32();
        _fileVersion = br.ReadInt32();
        _layoutRules = des.ReadByteCStyleArray(br, 4);
        _numSections = br.ReadInt32();
        _contentsSectionIndex = br.ReadInt32();
        _contentsSectionOffset = br.ReadInt32();
        _contentsClassNameSectionIndex = br.ReadInt32();
        _contentsClassNameSectionOffset = br.ReadInt32();
        _contentsVersion = des.ReadSByteCStyleArray(br, 16);
        _flags = br.ReadInt32();
        _maxpredicate = br.ReadUInt16();
        _predicateArraySizePlusPadding = br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteInt32CStyleArray(bw, _magic);
        bw.WriteInt32(_userTag);
        bw.WriteInt32(_fileVersion);
        s.WriteByteCStyleArray(bw, _layoutRules);
        bw.WriteInt32(_numSections);
        bw.WriteInt32(_contentsSectionIndex);
        bw.WriteInt32(_contentsSectionOffset);
        bw.WriteInt32(_contentsClassNameSectionIndex);
        bw.WriteInt32(_contentsClassNameSectionOffset);
        s.WriteSByteCStyleArray(bw, _contentsVersion);
        bw.WriteInt32(_flags);
        bw.WriteUInt16(_maxpredicate);
        bw.WriteUInt16(_predicateArraySizePlusPadding);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_magic), _magic);
        xs.WriteNumber(xe, nameof(_userTag), _userTag);
        xs.WriteNumber(xe, nameof(_fileVersion), _fileVersion);
        xs.WriteNumberArray(xe, nameof(_layoutRules), _layoutRules);
        xs.WriteNumber(xe, nameof(_numSections), _numSections);
        xs.WriteNumber(xe, nameof(_contentsSectionIndex), _contentsSectionIndex);
        xs.WriteNumber(xe, nameof(_contentsSectionOffset), _contentsSectionOffset);
        xs.WriteNumber(xe, nameof(_contentsClassNameSectionIndex), _contentsClassNameSectionIndex);
        xs.WriteNumber(xe, nameof(_contentsClassNameSectionOffset), _contentsClassNameSectionOffset);
        xs.WriteNumberArray(xe, nameof(_contentsVersion), _contentsVersion);
        xs.WriteNumber(xe, nameof(_flags), _flags);
        xs.WriteNumber(xe, nameof(_maxpredicate), _maxpredicate);
        xs.WriteNumber(xe, nameof(_predicateArraySizePlusPadding), _predicateArraySizePlusPadding);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkPackfileHeader);
    }
    public bool Equals(hkPackfileHeader? other)
    {
        return other is not null && _magic.Equals(other._magic) && _userTag.Equals(other._userTag) && _fileVersion.Equals(other._fileVersion) && _layoutRules.Equals(other._layoutRules) && _numSections.Equals(other._numSections) && _contentsSectionIndex.Equals(other._contentsSectionIndex) && _contentsSectionOffset.Equals(other._contentsSectionOffset) && _contentsClassNameSectionIndex.Equals(other._contentsClassNameSectionIndex) && _contentsClassNameSectionOffset.Equals(other._contentsClassNameSectionOffset) && _contentsVersion.Equals(other._contentsVersion) && _flags.Equals(other._flags) && _maxpredicate.Equals(other._maxpredicate) && _predicateArraySizePlusPadding.Equals(other._predicateArraySizePlusPadding) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_magic);
        code.Add(_userTag);
        code.Add(_fileVersion);
        code.Add(_layoutRules);
        code.Add(_numSections);
        code.Add(_contentsSectionIndex);
        code.Add(_contentsSectionOffset);
        code.Add(_contentsClassNameSectionIndex);
        code.Add(_contentsClassNameSectionOffset);
        code.Add(_contentsVersion);
        code.Add(_flags);
        code.Add(_maxpredicate);
        code.Add(_predicateArraySizePlusPadding);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
