using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldDrawLine : MonoBehaviour
{

    private LineRenderer line;
    private bool isMousePressed;
    public List<Vector3> pointsList;
    private Vector3 mousePos;
    public Material mat;
    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };

    private void Awake()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material = mat;
        line.SetVertexCount(0);
        line.SetWidth(0.1f, 0.1f);
        line.SetColors(Color.green, Color.green);
        line.useWorldSpace = true;
        isMousePressed = false;
        pointsList = new List<Vector3>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public int maxVertices = 100000;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
            line.SetVertexCount(0);
            pointsList.RemoveRange(0, pointsList.Count);
            line.SetColors(Color.green, Color.green);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }
        if (isMousePressed)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (!pointsList.Contains(mousePos))
            {
                if (pointsList.Count > maxVertices)
                {
                    pointsList.RemoveAt(0);

                }
                pointsList.Add(mousePos);
                line.SetVertexCount(pointsList.Count);
                line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
                if (isLineCollide())
                {
                    tomark.Enqueue(mousePos);
                    isMousePressed = false;
                    line.SetColors(Color.red, Color.red);
                }
            }
        }
    }
    Queue<Vector2> tomark = new Queue<Vector2>();

    private void OnDrawGizmos()
    {
        foreach (var v in tomark)
        {
            Gizmos.DrawSphere(v, 0.1f);
        }
    }
    private bool isLineCollide()
    {
        if (pointsList.Count < 2)
        {
            return false;
        }
        int TotalLines = pointsList.Count - 1;
        myLine[] lines = new myLine[TotalLines];
        if (TotalLines > 1)
        {
            for (int i = 0; i < TotalLines; i++)
            {
                lines[i].StartPoint = (Vector3)pointsList[i];
                lines[i].EndPoint = (Vector3)pointsList[i + 1];
            }
        }
        for (int i = 0; i < TotalLines - 1; i++)
        {
            myLine currentLine;
            currentLine.StartPoint = (Vector3)pointsList[pointsList.Count - 2];
            currentLine.EndPoint = (Vector3)pointsList[pointsList.Count - 1];
            if (isLinesIntersect(lines[i], currentLine))
            {
                return true;
            }

        }
        return false;
    }

    private bool checkPoints(Vector3 pointA, Vector3 pointB)
    {
        return (pointA.x == pointB.x && pointA.y == pointB.y);
    }

    private bool isLinesIntersect(myLine L1, myLine L2)
    {
        if (checkPoints(L1.StartPoint, L2.StartPoint) ||
                checkPoints(L1.StartPoint, L2.EndPoint) ||
                checkPoints(L1.EndPoint, L2.StartPoint) ||
                checkPoints(L1.EndPoint, L2.EndPoint))
            return false;

        return ((Mathf.Max(L1.StartPoint.x, L1.EndPoint.x) >= Mathf.Min(L2.StartPoint.x, L2.EndPoint.x)) &&
            (Mathf.Max(L2.StartPoint.x, L2.EndPoint.x) >= Mathf.Min(L1.StartPoint.x, L1.EndPoint.x)) &&
            (Mathf.Max(L1.StartPoint.y, L1.EndPoint.y) >= Mathf.Min(L2.StartPoint.y, L2.EndPoint.y)) &&
            (Mathf.Max(L2.StartPoint.y, L2.EndPoint.y) >= Mathf.Min(L1.StartPoint.y, L1.EndPoint.y)));
    }
}
