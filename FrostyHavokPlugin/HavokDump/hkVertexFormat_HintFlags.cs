using System;
namespace hk;
[Flags]
public enum hkVertexFormat_HintFlags : uint
{
    FLAG_READ = 1,
    FLAG_WRITE = 2,
    FLAG_DYNAMIC = 4,
    FLAG_NOT_SHARED = 8,
}
