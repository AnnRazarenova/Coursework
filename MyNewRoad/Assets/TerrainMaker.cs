using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static RoadPoints;

public class TerrainMaker : MonoBehaviour
{   
    private Terrain terrain;

    private float scale = 5f; // Масштаб для шума Перлина
    private float offsetX = 5f; // Смещение по X
    private float offsetY = 100f; // Смещение по Y

    private void Start()
    {
        terrain = GetComponent<Terrain>();

        // Проверяем, что terrain был правильно настроен в инспекторе
        if (terrain == null)
        {
            Debug.LogError("Terrain object is not assigned in TerrainGenerator!");
            return;
        }

        GenerateTerrain();
    }
    void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                float xCoord = (float)x / terrainData.heightmapResolution * scale + offsetX;
                float yCoord = (float)y / terrainData.heightmapResolution * scale + offsetY;

                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);
                heights[y, x] = perlinValue;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
}
