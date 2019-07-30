using UnityEngine;
using Sha_Throwaways;

namespace Sha_Throwaways
{

    public class SpawnRandomly : MonoBehaviour
    {
        //I have to spawn obstacles and enemies randomly. This will be called in the Start or Awake function because each time the game starts, we should choose new positions for the objects.
        public Vector2 center;
        public Vector2 size;

        int randNum;

        GameObject temp;
        public GameObject ObstaclePrefab;

        // Start is called before the first frame update
        void Start()
        {
            randNum = Random.Range(1, 5);
            for (int i = 0; i <= randNum; i++)
            {
                SpawnObstacles();
            }
        }

        public void SpawnObstacles()
        {
            Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            temp = Instantiate(ObstaclePrefab, pos, Quaternion.identity);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, .25f);
            Gizmos.DrawCube(center, size);
        }
    }
}
