using System;
namespace hk;
public enum hkxAttribute_Hint : uint
{
    HINT_NONE = 0,
    HINT_IGNORE = 1,
    HINT_TRANSFORM = 2,
    HINT_SCALE = 4,
    HINT_TRANSFORM_AND_SCALE = 6,
    HINT_FLIP = 8,
}
