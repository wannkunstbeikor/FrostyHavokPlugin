using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpSparseCompactMapunsignedshort : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _secondaryKeyMask;
    public uint _sencondaryKeyBits;
    public List<ushort> _primaryKeyToIndex = new();
    public List<ushort> _valueAndSecondaryKeys = new();
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _secondaryKeyMask = br.ReadUInt32();
        _sencondaryKeyBits = br.ReadUInt32();
        _primaryKeyToIndex = des.ReadUInt16Array(br);
        _valueAndSecondaryKeys = des.ReadUInt16Array(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_secondaryKeyMask);
        bw.WriteUInt32(_sencondaryKeyBits);
        s.WriteUInt16Array(bw, _primaryKeyToIndex);
        s.WriteUInt16Array(bw, _valueAndSecondaryKeys);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_secondaryKeyMask), _secondaryKeyMask);
        xs.WriteNumber(xe, nameof(_sencondaryKeyBits), _sencondaryKeyBits);
        xs.WriteNumberArray(xe, nameof(_primaryKeyToIndex), _primaryKeyToIndex);
        xs.WriteNumberArray(xe, nameof(_valueAndSecondaryKeys), _valueAndSecondaryKeys);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpSparseCompactMapunsignedshort other && _secondaryKeyMask == other._secondaryKeyMask && _sencondaryKeyBits == other._sencondaryKeyBits && _primaryKeyToIndex.SequenceEqual(other._primaryKeyToIndex) && _valueAndSecondaryKeys.SequenceEqual(other._valueAndSecondaryKeys) && Signature == other.Signature;
    }
    public static bool operator ==(hknpSparseCompactMapunsignedshort? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpSparseCompactMapunsignedshort? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_secondaryKeyMask);
        code.Add(_sencondaryKeyBits);
        code.Add(_primaryKeyToIndex);
        code.Add(_valueAndSecondaryKeys);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
