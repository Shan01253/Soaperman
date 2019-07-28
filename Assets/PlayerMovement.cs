using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    Rigidbody2D rb;

    public float speed = 10;
    public float smoothFactor = .2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = speed * Input.GetAxisRaw("Horizontal");
        verticalInput = speed * Input.GetAxisRaw("Vertical");

        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(horizontalInput, verticalInput), smoothFactor);
    }
}
