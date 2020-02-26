using System;
using UnityEngine;

[Serializable]
public struct PlanetTerrainTexture
{
    public Texture2D BaseColor;
    public Texture2D Normals;
    public Texture2D AmbientOcclusion;
    public Texture2D Roughness;
}
