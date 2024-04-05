using System;
namespace hk;
[Flags]
public enum hkMeshVertexBuffer_Flags : uint
{
    ACCESS_READ = 1,
    ACCESS_WRITE = 2,
    ACCESS_READ_WRITE = 3,
    ACCESS_WRITE_DISCARD = 4,
    ACCESS_ELEMENT_ARRAY = 8,
}
