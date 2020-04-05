using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnE : MonoBehaviour
{
    public static SpawnE manage;
    public Transform spawnPoint;
    public GameObject[] enemyPrefab;

    private GameObject enemyPos;

    private void Awake()
    {
        manage = this;
    }

    public void Spawn()
    {
        enemyPos = Instantiate(enemyPrefab[RandomPrefabIndex()], spawnPoint.position, spawnPoint.rotation) as GameObject;
    }

    int RandomPrefabIndex()
    {
        if (enemyPrefab.Length <= 1)
            return 0;
        int randomIndex;
        randomIndex = Random.Range(0, enemyPrefab.Length);
        return randomIndex;
    }
}
