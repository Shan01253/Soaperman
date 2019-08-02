using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class circle : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxRadius = 7;
    float radius = 2;
    public float speed = 5;
    public float restInterval = 5;
    public float restTime = 2;
    bool resting = false;
    Vector2 towardsCenter;
    public Vector2 center = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (!resting)
        {
            towardsCenter = (center - (Vector2)transform.position).normalized * speed;
            rb.velocity = (towardsCenter + radius * rb.velocity) / 2;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


}
