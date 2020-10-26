using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    public int mapSize=50;
    public float noiseScale = 3;
    public Tile tile;
    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        GenerateSurface();
    }

    void GenerateSurface()
    {
        for (int x = -mapSize; x < mapSize; x++)
        {
            Debug.Log(x);
            for (int y = 0; y < 10; y++)
            {
                float xCoord = (float)x *mapSize / noiseScale;
                float yCoord = (float)y *mapSize/ noiseScale;
                float noise = Mathf.PerlinNoise(xCoord, yCoord);

                if (noise > 0.5f)
                {
                    for (int z = 0; z < 10; z++)
                    {
                        Vector3Int pos = new Vector3Int((int)(x + transform.position.x), (int)(y + transform.position.y - z), 0);
                        tilemap.SetTile(tilemap.WorldToCell(pos), tile);
                    }
                    continue;
                }
            }
        }
    }
}
