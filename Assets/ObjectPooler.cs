using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // unity attributes
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    // grab objectPooler from cubeSpawner

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        Awake_two();
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Awake_two()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // initialize poolDictionary
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // spawn specified tag
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        //for some reason particles misbehave if they are active and are setactive redundantly
        if (objectToSpawn.activeInHierarchy) { objectToSpawn.SetActive(false); }
        objectToSpawn.SetActive(true);


        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            // if object derives from an interface, do special startup functions to set them up
            if (tag.Contains("enemy"))
            {
                pooledObj.onEnemySpawn();
            }
            else
            {
                pooledObj.onPlayerSpawn();
            }
        }

        // add tag back to queue for reuse
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
public interface IPooledObject
{
    void onPlayerSpawn();
    void onEnemySpawn();
}