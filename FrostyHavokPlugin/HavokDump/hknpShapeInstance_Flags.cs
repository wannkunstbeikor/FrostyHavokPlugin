using System;
namespace hk;
[Flags]
public enum hknpShapeInstance_Flags : uint
{
    HAS_TRANSLATION = 2,
    HAS_ROTATION = 4,
    HAS_SCALE = 8,
    FLIP_ORIENTATION = 16,
    SCALE_SURFACE = 32,
    IS_ENABLED = 64,
    DEFAULT_FLAGS = 64,
}
