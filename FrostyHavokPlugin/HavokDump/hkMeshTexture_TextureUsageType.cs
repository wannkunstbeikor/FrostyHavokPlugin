using System;
namespace hk;
public enum hkMeshTexture_TextureUsageType : uint
{
    UNKNOWN = 0,
    DIFFUSE = 1,
    REFLECTION = 2,
    BUMP = 3,
    NORMAL = 4,
    DISPLACEMENT = 5,
    SPECULAR = 6,
    SPECULARANDGLOSS = 7,
    OPACITY = 8,
    EMISSIVE = 9,
    REFRACTION = 10,
    GLOSS = 11,
    DOMINANTS = 12,
    NOTEXPORTED = 13,
}
