using System;
namespace hk;
public enum hknpCollisionDispatchType_Enum : uint
{
    NONE = 0,
    CONVEX = 1,
    COMPOSITE = 2,
    DISTANCE_FIELD = 3,
    USER = 4,
    NUM_TYPES = 5,
}
