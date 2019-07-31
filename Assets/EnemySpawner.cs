using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPositions;
    GameObject[] activeEnemies;
    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new GameObject[spawnPositions.Length];

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            activeEnemies[i] = ObjectPooler.Instance.SpawnFromPool("enemy", 
                spawnPositions[i].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
