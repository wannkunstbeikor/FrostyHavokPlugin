using System;
namespace hk;
public enum hknpWorldCinfo_LeavingBroadPhaseBehavior : uint
{
    ON_LEAVING_BROAD_PHASE_DO_NOTHING = 0,
    ON_LEAVING_BROAD_PHASE_REMOVE_BODY = 1,
    ON_LEAVING_BROAD_PHASE_FREEZE_BODY = 2,
}
