using System;
namespace hk;
public enum hkpConstraintMotor_MotorType : uint
{
    TYPE_INVALID = 0,
    TYPE_POSITION = 1,
    TYPE_VELOCITY = 2,
    TYPE_SPRING_DAMPER = 3,
    TYPE_CALLBACK = 4,
    TYPE_MAX = 5,
}
