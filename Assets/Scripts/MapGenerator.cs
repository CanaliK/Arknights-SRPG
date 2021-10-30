using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public Vector2 mapSize;
    public Transform mapHolder;

    void Start()
    {
        GenerateMap();
    }

    /// <summary>
    /// Éú³ÉµØÍ¼
    /// </summary>
    private void GenerateMap()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                Vector3 newPos = new Vector3(-mapSize.x / 2 + 0.5f + i, 0, -mapSize.y / 2 + .05f + j);
                GameObject spawnTile = Instantiate(cubePrefab, newPos, Quaternion.identity);
                spawnTile.transform.SetParent(mapHolder);
            }
        }
    }
}
