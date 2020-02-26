using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Settings")]
    public List<PlanetTerrainTexture> PlanetTextures;
    public FloatRange CloudSize;
    public List<Color> AtmosphereColors;

    [Header("References")]
    public MeshRenderer PlanetMeshRenderer;
    public MeshRenderer AtmosphereRenderer;

    void Start()
    {
        this.RandomizePlanet();
    }

    public void RandomizePlanet()
    {
        // Atmosphere
        Color atmosphereColor = this.AtmosphereColors[Random.Range(0, this.AtmosphereColors.Count - 1)];
        float cloudSize = Random.Range(this.CloudSize.Min, this.CloudSize.Max);
        this.AtmosphereRenderer.material.SetColor("_Color", atmosphereColor);
        this.AtmosphereRenderer.material.SetFloat("_CloudSize", cloudSize);

        // Planet
        PlanetTerrainTexture textureA = this.PlanetTextures[Random.Range(0, this.PlanetTextures.Count - 1)];
        PlanetTerrainTexture textureB = this.PlanetTextures[Random.Range(0, this.PlanetTextures.Count - 1)];
        this.PlanetMeshRenderer.material.SetTexture("_TextureAColor", textureA.BaseColor);
        this.PlanetMeshRenderer.material.SetTexture("_TextureANormals", textureA.Normals);
        this.PlanetMeshRenderer.material.SetTexture("_TextureAAO", textureA.AmbientOcclusion);
        this.PlanetMeshRenderer.material.SetTexture("_TextureARoughness", textureA.Roughness);
        this.PlanetMeshRenderer.material.SetTexture("_TextureBColor", textureB.BaseColor);
        this.PlanetMeshRenderer.material.SetTexture("_TextureBNormals", textureB.Normals);
        this.PlanetMeshRenderer.material.SetTexture("_TextureBAO", textureB.AmbientOcclusion);
        this.PlanetMeshRenderer.material.SetTexture("_TextureBRoughness", textureB.Roughness);
    }
}
