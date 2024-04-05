using System;
namespace hk;
public enum hknpCollisionFilter_FilterType : uint
{
    ALWAYS_HIT_FILTER = 0,
    CONSTRAINT_FILTER = 1,
    GROUP_FILTER = 2,
    PAIR_FILTER = 3,
    USER_FILTER = 4,
}
