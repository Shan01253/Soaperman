using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class enemy : MonoBehaviour
{
    public float redflashduration = 0.3f;
    SpriteRenderer sprite;
    public event Action onKillListeners;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public float healthPercentage = 1;
    bool isDead = false;
    public void kill()
    {
        onKillListeners?.Invoke();
        BasicScoring.Instance.addTimeToClock(3);
        isDead = true;
        healthPercentage = 1;
        //Debug.Log("dead?");
        StartCoroutine(flashRed());
        gameObject.SetActive(false);
    }

    public void damage(float amount)
    {
        if (!isDead)
        {
            //Debug.Log("damage");
            //Debug.Log(healthPercentage);
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
