using System;
namespace hk;
public enum hknpMaterial_TriggerType : uint
{
    TRIGGER_TYPE_NONE = 0,
    TRIGGER_TYPE_BROAD_PHASE = 1,
    TRIGGER_TYPE_NARROW_PHASE = 2,
    TRIGGER_TYPE_CONTACT_SOLVER = 3,
}
