using System;
namespace hk;
public enum hkpPointToPathConstraintData_OrientationConstraintType : uint
{
    CONSTRAIN_ORIENTATION_INVALID = 0,
    CONSTRAIN_ORIENTATION_NONE = 1,
    CONSTRAIN_ORIENTATION_ALLOW_SPIN = 2,
    CONSTRAIN_ORIENTATION_TO_PATH = 3,
    CONSTRAIN_ORIENTATION_MAX_ID = 4,
}
