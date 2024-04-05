using System;
namespace hk;
[Flags]
public enum hknpMotionProperties_FlagsEnum : uint
{
    NEVER_REBUILD_MASS_PROPERTIES = 2,
    ENABLE_GRAVITY_MODIFICATION = 536870912,
    ENABLE_TIME_FACTOR = 1073741824,
    FLAGS_MASK = 3758096384,
    AUTO_FLAGS_MASK = 66060288,
}
