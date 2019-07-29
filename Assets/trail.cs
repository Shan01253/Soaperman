using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class trail : MonoBehaviour
{
    public Vector2 lastPosition;
    public float deltaDistance = 0.1f;
    public float timeLength = 2;
    TrailRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        renderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame

    void Update()
    {

        if (Vector2.Distance(lastPosition, transform.position) > deltaDistance)
        {
            lastPosition = transform.position;
            tomark.Enqueue(transform.position);
        }
    }
    Queue<Vector2> tomark = new Queue<Vector2>();
    private void OnDrawGizmos()
    {
        if (tomark.Count > 0)
        {
            foreach (Vector2 v in tomark)
            Gizmos.DrawSphere(v, 0.2f);
        }
    }
}
