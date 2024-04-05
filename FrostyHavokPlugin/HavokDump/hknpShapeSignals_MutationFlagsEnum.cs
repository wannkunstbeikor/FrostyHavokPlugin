using System;
namespace hk;
[Flags]
public enum hknpShapeSignals_MutationFlagsEnum : uint
{
    MUTATION_AABB_CHANGED = 1,
    MUTATION_DISCARD_CACHED_DISTANCES = 2,
    MUTATION_REBUILD_COLLISION_CACHES = 4,
}
