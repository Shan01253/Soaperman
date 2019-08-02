using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPositions;
    GameObject[] activeEnemies;
    public static event Action killedEnemiesCallback;
    public int enemiesAtATime = 5;
    // Start is called before the first frame update
    int killcount = 0;
    void Start()
    {
        //TODO remember to implement choosing enemy positions
        for (int i = 0; i < enemiesAtATime; i++)
        {
            ObjectPooler.Instance.SpawnFromPool("enemy", 
                new Vector2(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 8)), 
                Quaternion.identity).GetComponent<enemy>().onKillListeners += incKillCount;
        }

    }

    public void incKillCount()
    {
        killcount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (killcount >= enemiesAtATime)
        {
            killcount = 0;
            for (int i = 0; i < enemiesAtATime; i++)
            {
                ObjectPooler.Instance.SpawnFromPool("enemy",
                    new Vector2(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 8)),
                    Quaternion.identity).GetComponent<enemy>().onKillListeners += incKillCount;
            }
        }
    }

    public void spawnEnemies()
    {

    }
}
