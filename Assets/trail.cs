using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class trail : MonoBehaviour
{
    public Vector2 lastPosition;
    public float deltaDistance = 1f;
    public float timeLength = 2;
    TrailRenderer renderer;
    // Start is called before the first frame update

    struct lineSegment
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
    }
   
    void Start()
    {

        lastPosition = transform.position;
        //renderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame

    void Update()
    {

        if (trailCollided())
        {
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

    Vector3 lastDeltaDistance;
    private bool trailCollided()
    {
        if (renderer.positionCount < 3)
        {
            if (renderer.positionCount > 0)
            {
                lastDeltaDistance = renderer.GetPosition(renderer.positionCount - 1);
            }
            return false;
        }

        if (Vector2.Distance(lastDeltaDistance, renderer.GetPosition(renderer.positionCount - 1)) < deltaDistance)
        {
            return false;
        }
        lastDeltaDistance = renderer.GetPosition(renderer.positionCount - 1);
        lineSegment[] segs = new lineSegment[renderer.positionCount - 1];
        if (renderer.positionCount > 1)
        {
            for (int i = 0; i < segs.Length; i++)
            {
                segs[i].StartPoint = renderer.GetPosition(i);
                segs[i].EndPoint = renderer.GetPosition(i + 1);
            }
        }

        for (int i = 0; i < segs.Length; i++)
        {
            lineSegment currentSegment = new lineSegment();
            currentSegment.StartPoint = renderer.GetPosition(renderer.positionCount - 2);
            currentSegment.StartPoint = renderer.GetPosition(renderer.positionCount - 1);

            if (doSegmentsIntersect(segs[i], currentSegment))
            {
                return true;
            }
        }
        return false;
    }


    private bool doSegmentsIntersect(lineSegment a, lineSegment b)
    {
        if (a.StartPoint == b.StartPoint ||
            a.StartPoint == b.EndPoint ||
            a.EndPoint == b.StartPoint ||
            a.EndPoint == b.EndPoint)
        {
            return false;
        }

        return ((Mathf.Max(a.StartPoint.x, a.EndPoint.x) >= Mathf.Min(b.StartPoint.x, b.EndPoint.x)) && 
                (Mathf.Max(b.StartPoint.x, b.EndPoint.x) >= Mathf.Min(a.StartPoint.x, a.EndPoint.x)) &&
                (Mathf.Max(a.StartPoint.y, a.EndPoint.y) >= Mathf.Min(b.StartPoint.y, b.EndPoint.y)) &&
                (Mathf.Max(b.StartPoint.y, b.EndPoint.y) >= Mathf.Min(a.StartPoint.y, a.EndPoint.y)));
    }
}
