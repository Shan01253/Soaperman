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
    public int currentAmountEnemies;
    // Start is called before the first frame update
    int killcount = 0;
    
    void Start()
    {
        currentAmountEnemies = enemiesAtATime;
        //TODO remember to implement choosing enemy positions
        for (int i = 0; i < enemiesAtATime; i++)
        {
            enemy e = ObjectPooler.Instance.SpawnFromPool("enemy", 
                new Vector2(UnityEngine.Random.Range(-6, 6), UnityEngine.Random.Range(-4, 8)), 
                Quaternion.identity).GetComponent<enemy>();
            e.onKillListeners += incKillCount;
           EB_BackAndForth EB = e.GetComponent<EB_BackAndForth>();
            EB.EnemyDirection = (EB_BackAndForth.Direction) UnityEngine.Random.Range(0, 3);
            EB.EnemySpeed = UnityEngine.Random.Range(-1f, 1f) + EB.EnemySpeed;
            EB.PauseInterval = UnityEngine.Random.Range(0, 3);
            EB.WalkInterval = UnityEngine.Random.Range(2, 5);
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
            currentAmountEnemies++;
            killcount = 0;
            for (int i = 0; i < currentAmountEnemies; i++)
            {
                ObjectPooler.Instance.SpawnFromPool("enemy",
                    new Vector2(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5)),
                    Quaternion.identity).GetComponent<enemy>().onKillListeners += incKillCount;
            }
        }
    }

    public void spawnEnemies()
    {

    }
}
