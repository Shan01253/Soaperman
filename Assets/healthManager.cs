using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class healthManager : MonoBehaviour
{
    Rigidbody2D rb;
    public float enemyRepulsionFactor = 1;
    public float currentHealthPercent = 1;
    public float remainingHearts = 3;
    public float damageFromEnemies = 1;
    bool canBeDamaged = true;
    SpriteRenderer sprite;
    public void Damage(float percentAmount)
    {
        if (currentHealthPercent - percentAmount <= 0)
        {
            currentHealthPercent = 1;
            remainingHearts--;
            if (remainingHearts <= 0)
            {
                Die();
            }
            heartbar.Instance.DepleteHeart();


        }
        else
        {
            currentHealthPercent -= percentAmount;
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (canBeDamaged && collision.gameObject.CompareTag("enemy"))
        {
            rb.AddForce(-new Vector2(collision.transform.position.x, collision.transform.position.y)*enemyRepulsionFactor, ForceMode2D.Impulse);
            Damage(damageFromEnemies);
        }
        StartCoroutine(invulnerability());
    }

    IEnumerator invulnerability()
    {
        canBeDamaged = false;
        for (int i = 0; i < 10; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        canBeDamaged = true;
    }
}
