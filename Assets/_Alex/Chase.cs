using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chase : MonoBehaviour
{
    public float speed = 5;
    public float averageRestTime = 2;
    public float averageRestInterval = 5;
    public Transform Target;
    private Vector2 velocity;
    bool resting = false;
    Rigidbody2D rb; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(restingController());
    }

    // Update is called once per frame
    void Update()
    {
        if (!resting)
        {
            velocity = Target.transform.position - transform.position;
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    IEnumerator restingController()
    {
        if (averageRestTime > 0)
        {
            while (true)
            {
                yield return new WaitForSeconds(averageRestInterval);
                resting = true;
                yield return new WaitForSeconds(averageRestTime);
                resting = false;
            }
        }
    }
}
