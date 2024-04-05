using System;
namespace hk;
public enum hkContactPointMaterial_FlagEnum : uint
{
    CONTACT_IS_NEW = 1,
    CONTACT_USES_SOLVER_PATH2 = 2,
    CONTACT_BREAKOFF_OBJECT_ID_SMALLER = 4,
    CONTACT_IS_DISABLED = 8,
}
