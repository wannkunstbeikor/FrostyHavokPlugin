using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpWorldCinfo : IHavokObject
{
    public virtual uint Signature => 0;
    public int _bodyBufferCapacity;
    // TYPE_POINTER TYPE_VOID _userBodyBuffer
    public int _motionBufferCapacity;
    // TYPE_POINTER TYPE_VOID _userMotionBuffer
    public int _constraintBufferCapacity;
    // TYPE_POINTER TYPE_VOID _userConstraintBuffer
    // TYPE_POINTER TYPE_VOID _persistentStreamAllocator
    public hknpMaterialLibrary? _materialLibrary;
    public hknpMotionPropertiesLibrary? _motionPropertiesLibrary;
    public hknpBodyQualityLibrary? _qualityLibrary;
    public hkAabb? _broadPhaseAabb;
    public hknpWorldCinfo_LeavingBroadPhaseBehavior _leavingBroadPhaseBehavior;
    public hknpBroadPhaseConfig? _broadPhaseConfig;
    public hknpCollisionFilter? _collisionFilter;
    public hknpShapeTagCodec? _shapeTagCodec;
    public float _collisionTolerance;
    public float _relativeCollisionAccuracy;
    public Vector4 _gravity;
    public int _solverIterations;
    public float _solverTau;
    public float _solverDamp;
    public int _solverMicrosteps;
    public float _defaultSolverTimestep;
    public float _maxApproachSpeedForHighQualitySolver;
    public bool _enableSolverDynamicScheduling;
    public hknpWorldCinfo_SimulationType _simulationType;
    public int _numSplitterCells;
    public bool _mergeEventsBeforeDispatch;
    public bool _enableDeactivation;
    public int _largeIslandSize;
    public float _unitScale;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _bodyBufferCapacity = br.ReadInt32();
        br.Position += 12; // padding
        _motionBufferCapacity = br.ReadInt32();
        br.Position += 12; // padding
        _constraintBufferCapacity = br.ReadInt32();
        br.Position += 20; // padding
        _materialLibrary = des.ReadClassPointer<hknpMaterialLibrary>(br);
        _motionPropertiesLibrary = des.ReadClassPointer<hknpMotionPropertiesLibrary>(br);
        _qualityLibrary = des.ReadClassPointer<hknpBodyQualityLibrary>(br);
        _broadPhaseAabb = new hkAabb();
        _broadPhaseAabb.Read(des, br);
        _leavingBroadPhaseBehavior = (hknpWorldCinfo_LeavingBroadPhaseBehavior)br.ReadByte();
        br.Position += 7; // padding
        _broadPhaseConfig = des.ReadClassPointer<hknpBroadPhaseConfig>(br);
        _collisionFilter = des.ReadClassPointer<hknpCollisionFilter>(br);
        _shapeTagCodec = des.ReadClassPointer<hknpShapeTagCodec>(br);
        _collisionTolerance = br.ReadSingle();
        _relativeCollisionAccuracy = br.ReadSingle();
        br.Position += 8; // padding
        _gravity = des.ReadVector4(br);
        _solverIterations = br.ReadInt32();
        _solverTau = br.ReadSingle();
        _solverDamp = br.ReadSingle();
        _solverMicrosteps = br.ReadInt32();
        _defaultSolverTimestep = br.ReadSingle();
        _maxApproachSpeedForHighQualitySolver = br.ReadSingle();
        _enableSolverDynamicScheduling = br.ReadBoolean();
        _simulationType = (hknpWorldCinfo_SimulationType)br.ReadByte();
        br.Position += 2; // padding
        _numSplitterCells = br.ReadInt32();
        _mergeEventsBeforeDispatch = br.ReadBoolean();
        _enableDeactivation = br.ReadBoolean();
        br.Position += 2; // padding
        _largeIslandSize = br.ReadInt32();
        _unitScale = br.ReadSingle();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_bodyBufferCapacity);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_motionBufferCapacity);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_constraintBufferCapacity);
        for (int i = 0; i < 20; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hknpMaterialLibrary>(bw, _materialLibrary);
        s.WriteClassPointer<hknpMotionPropertiesLibrary>(bw, _motionPropertiesLibrary);
        s.WriteClassPointer<hknpBodyQualityLibrary>(bw, _qualityLibrary);
        _broadPhaseAabb.Write(s, bw);
        bw.WriteByte((byte)_leavingBroadPhaseBehavior);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hknpBroadPhaseConfig>(bw, _broadPhaseConfig);
        s.WriteClassPointer<hknpCollisionFilter>(bw, _collisionFilter);
        s.WriteClassPointer<hknpShapeTagCodec>(bw, _shapeTagCodec);
        bw.WriteSingle(_collisionTolerance);
        bw.WriteSingle(_relativeCollisionAccuracy);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _gravity);
        bw.WriteInt32(_solverIterations);
        bw.WriteSingle(_solverTau);
        bw.WriteSingle(_solverDamp);
        bw.WriteInt32(_solverMicrosteps);
        bw.WriteSingle(_defaultSolverTimestep);
        bw.WriteSingle(_maxApproachSpeedForHighQualitySolver);
        bw.WriteBoolean(_enableSolverDynamicScheduling);
        bw.WriteByte((byte)_simulationType);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numSplitterCells);
        bw.WriteBoolean(_mergeEventsBeforeDispatch);
        bw.WriteBoolean(_enableDeactivation);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_largeIslandSize);
        bw.WriteSingle(_unitScale);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_bodyBufferCapacity), _bodyBufferCapacity);
        xs.WriteNumber(xe, nameof(_motionBufferCapacity), _motionBufferCapacity);
        xs.WriteNumber(xe, nameof(_constraintBufferCapacity), _constraintBufferCapacity);
        xs.WriteClassPointer(xe, nameof(_materialLibrary), _materialLibrary);
        xs.WriteClassPointer(xe, nameof(_motionPropertiesLibrary), _motionPropertiesLibrary);
        xs.WriteClassPointer(xe, nameof(_qualityLibrary), _qualityLibrary);
        xs.WriteClass(xe, nameof(_broadPhaseAabb), _broadPhaseAabb);
        xs.WriteEnum<hknpWorldCinfo_LeavingBroadPhaseBehavior, byte>(xe, nameof(_leavingBroadPhaseBehavior), (byte)_leavingBroadPhaseBehavior);
        xs.WriteClassPointer(xe, nameof(_broadPhaseConfig), _broadPhaseConfig);
        xs.WriteClassPointer(xe, nameof(_collisionFilter), _collisionFilter);
        xs.WriteClassPointer(xe, nameof(_shapeTagCodec), _shapeTagCodec);
        xs.WriteFloat(xe, nameof(_collisionTolerance), _collisionTolerance);
        xs.WriteFloat(xe, nameof(_relativeCollisionAccuracy), _relativeCollisionAccuracy);
        xs.WriteVector4(xe, nameof(_gravity), _gravity);
        xs.WriteNumber(xe, nameof(_solverIterations), _solverIterations);
        xs.WriteFloat(xe, nameof(_solverTau), _solverTau);
        xs.WriteFloat(xe, nameof(_solverDamp), _solverDamp);
        xs.WriteNumber(xe, nameof(_solverMicrosteps), _solverMicrosteps);
        xs.WriteFloat(xe, nameof(_defaultSolverTimestep), _defaultSolverTimestep);
        xs.WriteFloat(xe, nameof(_maxApproachSpeedForHighQualitySolver), _maxApproachSpeedForHighQualitySolver);
        xs.WriteBoolean(xe, nameof(_enableSolverDynamicScheduling), _enableSolverDynamicScheduling);
        xs.WriteEnum<hknpWorldCinfo_SimulationType, byte>(xe, nameof(_simulationType), (byte)_simulationType);
        xs.WriteNumber(xe, nameof(_numSplitterCells), _numSplitterCells);
        xs.WriteBoolean(xe, nameof(_mergeEventsBeforeDispatch), _mergeEventsBeforeDispatch);
        xs.WriteBoolean(xe, nameof(_enableDeactivation), _enableDeactivation);
        xs.WriteNumber(xe, nameof(_largeIslandSize), _largeIslandSize);
        xs.WriteFloat(xe, nameof(_unitScale), _unitScale);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpWorldCinfo other && _bodyBufferCapacity == other._bodyBufferCapacity && _motionBufferCapacity == other._motionBufferCapacity && _constraintBufferCapacity == other._constraintBufferCapacity && _materialLibrary == other._materialLibrary && _motionPropertiesLibrary == other._motionPropertiesLibrary && _qualityLibrary == other._qualityLibrary && _broadPhaseAabb == other._broadPhaseAabb && _leavingBroadPhaseBehavior == other._leavingBroadPhaseBehavior && _broadPhaseConfig == other._broadPhaseConfig && _collisionFilter == other._collisionFilter && _shapeTagCodec == other._shapeTagCodec && _collisionTolerance == other._collisionTolerance && _relativeCollisionAccuracy == other._relativeCollisionAccuracy && _gravity == other._gravity && _solverIterations == other._solverIterations && _solverTau == other._solverTau && _solverDamp == other._solverDamp && _solverMicrosteps == other._solverMicrosteps && _defaultSolverTimestep == other._defaultSolverTimestep && _maxApproachSpeedForHighQualitySolver == other._maxApproachSpeedForHighQualitySolver && _enableSolverDynamicScheduling == other._enableSolverDynamicScheduling && _simulationType == other._simulationType && _numSplitterCells == other._numSplitterCells && _mergeEventsBeforeDispatch == other._mergeEventsBeforeDispatch && _enableDeactivation == other._enableDeactivation && _largeIslandSize == other._largeIslandSize && _unitScale == other._unitScale && Signature == other.Signature;
    }
    public static bool operator ==(hknpWorldCinfo? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpWorldCinfo? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bodyBufferCapacity);
        code.Add(_motionBufferCapacity);
        code.Add(_constraintBufferCapacity);
        code.Add(_materialLibrary);
        code.Add(_motionPropertiesLibrary);
        code.Add(_qualityLibrary);
        code.Add(_broadPhaseAabb);
        code.Add(_leavingBroadPhaseBehavior);
        code.Add(_broadPhaseConfig);
        code.Add(_collisionFilter);
        code.Add(_shapeTagCodec);
        code.Add(_collisionTolerance);
        code.Add(_relativeCollisionAccuracy);
        code.Add(_gravity);
        code.Add(_solverIterations);
        code.Add(_solverTau);
        code.Add(_solverDamp);
        code.Add(_solverMicrosteps);
        code.Add(_defaultSolverTimestep);
        code.Add(_maxApproachSpeedForHighQualitySolver);
        code.Add(_enableSolverDynamicScheduling);
        code.Add(_simulationType);
        code.Add(_numSplitterCells);
        code.Add(_mergeEventsBeforeDispatch);
        code.Add(_enableDeactivation);
        code.Add(_largeIslandSize);
        code.Add(_unitScale);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
