using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnX = 0;
    private float tileLength = 5.5f;
    private float safeZone = 6.2f;
    private int amnTilesOnScreen = 1;
    private int firstPrefabIndex = 0;

    private List<GameObject> activeTiles;

    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;


        for (int i = 0; i < amnTilesOnScreen; i++)
        {
          //  if (i < 1)
           //     SpawnTile(0);
          //  else
                SpawnTile();
        }

    }

    void Update()
    {
        if (playerTransform.position.x - safeZone > (spawnX - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)

            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;

        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.right * spawnX;
        spawnX += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()

    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = firstPrefabIndex;
        while (randomIndex == firstPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        firstPrefabIndex = randomIndex;
        return randomIndex;
    }
}
