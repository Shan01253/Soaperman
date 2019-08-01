using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class enemy : MonoBehaviour
{
    public float redflashduration = 0.3f;
    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public float healthPercentage = 1;
    bool isDead = false;
    public void kill()
    {
        isDead = true;
        healthPercentage = 1;
        Debug.Log("dead?");
        StartCoroutine(flashRed());
        gameObject.SetActive(false);
    }

    public void damage(float amount)
    {
        if (!isDead)
        {
            Debug.Log("damage");
            Debug.Log(healthPercentage);
            if (healthPercentage - amount <= 0)
            {
                kill();
            }
            else
            {
                healthPercentage -= amount;
                StartCoroutine(flashRed());
            }

        }

    }
    IEnumerator flashRed()
    {
        Color c = sprite.color;

        sprite.color = Color.red;
        yield return new WaitForSeconds(redflashduration);
        sprite.color = c;
    }
}
