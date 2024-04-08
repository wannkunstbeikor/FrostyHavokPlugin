using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBodyQuality : IHavokObject, IEquatable<hknpBodyQuality?>
{
    public virtual uint Signature => 0;
    public int _priority;
    public hknpBodyQuality_FlagsEnum _supportedFlags;
    public hknpBodyQuality_FlagsEnum _requestedFlags;
    public float _liveJacobianDistanceThreshold;
    public float _liveJacobianAngleThreshold;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _priority = br.ReadInt32();
        _supportedFlags = (hknpBodyQuality_FlagsEnum)br.ReadUInt32();
        _requestedFlags = (hknpBodyQuality_FlagsEnum)br.ReadUInt32();
        _liveJacobianDistanceThreshold = br.ReadSingle();
        _liveJacobianAngleThreshold = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_priority);
        bw.WriteUInt32((uint)_supportedFlags);
        bw.WriteUInt32((uint)_requestedFlags);
        bw.WriteSingle(_liveJacobianDistanceThreshold);
        bw.WriteSingle(_liveJacobianAngleThreshold);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_priority), _priority);
        xs.WriteFlag<hknpBodyQuality_FlagsEnum, uint>(xe, nameof(_supportedFlags), (uint)_supportedFlags);
        xs.WriteFlag<hknpBodyQuality_FlagsEnum, uint>(xe, nameof(_requestedFlags), (uint)_requestedFlags);
        xs.WriteFloat(xe, nameof(_liveJacobianDistanceThreshold), _liveJacobianDistanceThreshold);
        xs.WriteFloat(xe, nameof(_liveJacobianAngleThreshold), _liveJacobianAngleThreshold);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpBodyQuality);
    }
    public bool Equals(hknpBodyQuality? other)
    {
        return other is not null && _priority.Equals(other._priority) && _supportedFlags.Equals(other._supportedFlags) && _requestedFlags.Equals(other._requestedFlags) && _liveJacobianDistanceThreshold.Equals(other._liveJacobianDistanceThreshold) && _liveJacobianAngleThreshold.Equals(other._liveJacobianAngleThreshold) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_priority);
        code.Add(_supportedFlags);
        code.Add(_requestedFlags);
        code.Add(_liveJacobianDistanceThreshold);
        code.Add(_liveJacobianAngleThreshold);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
