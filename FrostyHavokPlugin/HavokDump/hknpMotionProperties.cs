using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMotionProperties : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _isExclusive;
    public hknpMotionProperties_FlagsEnum _flags;
    public float _gravityFactor;
    public float _timeFactor;
    public float _maxLinearSpeed;
    public float _maxAngularSpeed;
    public float _linearDamping;
    public float _angularDamping;
    public float _solverStabilizationSpeedThreshold;
    public float _solverStabilizationSpeedReduction;
    public float _maxDistSqrd;
    public float _maxRotSqrd;
    public float _invBlockSize;
    public short _pathingUpperThreshold;
    public short _pathingLowerThreshold;
    public byte _numDeactivationFrequencyPasses;
    public byte _deactivationVelocityScaleSquare;
    public byte _minimumPathingVelocityScaleSquare;
    public byte _spikingVelocityScaleThresholdSquared;
    public byte _minimumSpikingVelocityScaleSquared;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _isExclusive = br.ReadUInt32();
        _flags = (hknpMotionProperties_FlagsEnum)br.ReadUInt32();
        _gravityFactor = br.ReadSingle();
        _timeFactor = br.ReadSingle();
        _maxLinearSpeed = br.ReadSingle();
        _maxAngularSpeed = br.ReadSingle();
        _linearDamping = br.ReadSingle();
        _angularDamping = br.ReadSingle();
        _solverStabilizationSpeedThreshold = br.ReadSingle();
        _solverStabilizationSpeedReduction = br.ReadSingle();
        _maxDistSqrd = br.ReadSingle();
        _maxRotSqrd = br.ReadSingle();
        _invBlockSize = br.ReadSingle();
        _pathingUpperThreshold = br.ReadInt16();
        _pathingLowerThreshold = br.ReadInt16();
        _numDeactivationFrequencyPasses = br.ReadByte();
        _deactivationVelocityScaleSquare = br.ReadByte();
        _minimumPathingVelocityScaleSquare = br.ReadByte();
        _spikingVelocityScaleThresholdSquared = br.ReadByte();
        _minimumSpikingVelocityScaleSquared = br.ReadByte();
        br.Position += 3; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_isExclusive);
        bw.WriteUInt32((uint)_flags);
        bw.WriteSingle(_gravityFactor);
        bw.WriteSingle(_timeFactor);
        bw.WriteSingle(_maxLinearSpeed);
        bw.WriteSingle(_maxAngularSpeed);
        bw.WriteSingle(_linearDamping);
        bw.WriteSingle(_angularDamping);
        bw.WriteSingle(_solverStabilizationSpeedThreshold);
        bw.WriteSingle(_solverStabilizationSpeedReduction);
        bw.WriteSingle(_maxDistSqrd);
        bw.WriteSingle(_maxRotSqrd);
        bw.WriteSingle(_invBlockSize);
        bw.WriteInt16(_pathingUpperThreshold);
        bw.WriteInt16(_pathingLowerThreshold);
        bw.WriteByte(_numDeactivationFrequencyPasses);
        bw.WriteByte(_deactivationVelocityScaleSquare);
        bw.WriteByte(_minimumPathingVelocityScaleSquare);
        bw.WriteByte(_spikingVelocityScaleThresholdSquared);
        bw.WriteByte(_minimumSpikingVelocityScaleSquared);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_isExclusive), _isExclusive);
        xs.WriteFlag<hknpMotionProperties_FlagsEnum, uint>(xe, nameof(_flags), (uint)_flags);
        xs.WriteFloat(xe, nameof(_gravityFactor), _gravityFactor);
        xs.WriteFloat(xe, nameof(_timeFactor), _timeFactor);
        xs.WriteFloat(xe, nameof(_maxLinearSpeed), _maxLinearSpeed);
        xs.WriteFloat(xe, nameof(_maxAngularSpeed), _maxAngularSpeed);
        xs.WriteFloat(xe, nameof(_linearDamping), _linearDamping);
        xs.WriteFloat(xe, nameof(_angularDamping), _angularDamping);
        xs.WriteFloat(xe, nameof(_solverStabilizationSpeedThreshold), _solverStabilizationSpeedThreshold);
        xs.WriteFloat(xe, nameof(_solverStabilizationSpeedReduction), _solverStabilizationSpeedReduction);
        xs.WriteFloat(xe, nameof(_maxDistSqrd), _maxDistSqrd);
        xs.WriteFloat(xe, nameof(_maxRotSqrd), _maxRotSqrd);
        xs.WriteFloat(xe, nameof(_invBlockSize), _invBlockSize);
        xs.WriteNumber(xe, nameof(_pathingUpperThreshold), _pathingUpperThreshold);
        xs.WriteNumber(xe, nameof(_pathingLowerThreshold), _pathingLowerThreshold);
        xs.WriteNumber(xe, nameof(_numDeactivationFrequencyPasses), _numDeactivationFrequencyPasses);
        xs.WriteNumber(xe, nameof(_deactivationVelocityScaleSquare), _deactivationVelocityScaleSquare);
        xs.WriteNumber(xe, nameof(_minimumPathingVelocityScaleSquare), _minimumPathingVelocityScaleSquare);
        xs.WriteNumber(xe, nameof(_spikingVelocityScaleThresholdSquared), _spikingVelocityScaleThresholdSquared);
        xs.WriteNumber(xe, nameof(_minimumSpikingVelocityScaleSquared), _minimumSpikingVelocityScaleSquared);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpMotionProperties other && _isExclusive == other._isExclusive && _flags == other._flags && _gravityFactor == other._gravityFactor && _timeFactor == other._timeFactor && _maxLinearSpeed == other._maxLinearSpeed && _maxAngularSpeed == other._maxAngularSpeed && _linearDamping == other._linearDamping && _angularDamping == other._angularDamping && _solverStabilizationSpeedThreshold == other._solverStabilizationSpeedThreshold && _solverStabilizationSpeedReduction == other._solverStabilizationSpeedReduction && _maxDistSqrd == other._maxDistSqrd && _maxRotSqrd == other._maxRotSqrd && _invBlockSize == other._invBlockSize && _pathingUpperThreshold == other._pathingUpperThreshold && _pathingLowerThreshold == other._pathingLowerThreshold && _numDeactivationFrequencyPasses == other._numDeactivationFrequencyPasses && _deactivationVelocityScaleSquare == other._deactivationVelocityScaleSquare && _minimumPathingVelocityScaleSquare == other._minimumPathingVelocityScaleSquare && _spikingVelocityScaleThresholdSquared == other._spikingVelocityScaleThresholdSquared && _minimumSpikingVelocityScaleSquared == other._minimumSpikingVelocityScaleSquared && Signature == other.Signature;
    }
    public static bool operator ==(hknpMotionProperties? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMotionProperties? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isExclusive);
        code.Add(_flags);
        code.Add(_gravityFactor);
        code.Add(_timeFactor);
        code.Add(_maxLinearSpeed);
        code.Add(_maxAngularSpeed);
        code.Add(_linearDamping);
        code.Add(_angularDamping);
        code.Add(_solverStabilizationSpeedThreshold);
        code.Add(_solverStabilizationSpeedReduction);
        code.Add(_maxDistSqrd);
        code.Add(_maxRotSqrd);
        code.Add(_invBlockSize);
        code.Add(_pathingUpperThreshold);
        code.Add(_pathingLowerThreshold);
        code.Add(_numDeactivationFrequencyPasses);
        code.Add(_deactivationVelocityScaleSquare);
        code.Add(_minimumPathingVelocityScaleSquare);
        code.Add(_spikingVelocityScaleThresholdSquared);
        code.Add(_minimumSpikingVelocityScaleSquared);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
