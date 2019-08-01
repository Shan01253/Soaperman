using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshDamageEnemy : MonoBehaviour
{
    public float percentDamageDealt = 0;
    public int combo = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        combo++;
        Debug.Log("collide!");
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("kill!");
            collision.gameObject.GetComponent<enemy>().damage(percentDamageDealt);
            BasicScoring.Instance.increaseCurrentScore(1 + combo);
        }
    }

}
