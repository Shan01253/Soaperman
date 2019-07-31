using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TrailRenderer))]
public class collisionDetection : MonoBehaviour
{
    public float intersectionDeltaDistance = 0.01f;
    MeshFilter meshfilter_reverse;
    float timeToDeleteIntersections = 2;
    TrailRenderer rend;
    Vector3 lastPosition;
    public Shader shader;
    public Color IntersectionPolygonColor;

    Vector2 lastIntersection1;
    Vector2 lastIntersection2;
    int intersectIndex;

    float startingPercentDamageToEnemies = 0.5f;

    Queue<Vector2> tomark = new Queue<Vector2>();
    public float deltaDistance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        rend = GetComponent<TrailRenderer>();
    }

    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };

    // Update is called once per frame
    void Update()
    {
        if (isLineCollide())
        {
            if (Vector2.Distance(lastPosition, rend.GetPosition(rend.positionCount - 1)) > deltaDistance)
            {
                lastPosition = rend.GetPosition(rend.positionCount - 1);

                StartCoroutine(drawPolygonFromIntersection(lastIntersection1, intersectIndex));
            }

        }
    }
    IEnumerator drawPolygonFromIntersection(Vector3 intersection, int intersectIndex)
    {
        yield return null;
        Vector3[] positions = new Vector3[1000];

        int numberPositions = rend.GetPositions(positions);
        Vector2[] polygonPositions = new Vector2[numberPositions - intersectIndex];

        for (int j = intersectIndex; j < numberPositions; j++)
        {

            polygonPositions[j - intersectIndex] = positions[j];

        }

        GameObject meshObject = new GameObject("mesh");       

        meshObject.transform.parent = null;
        meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(shader);

        meshObject.tag = "soapmesh";


        MeshFilter meshfilter = meshObject.AddComponent<MeshFilter>();
        meshfilter.sharedMesh = new Mesh();
        
        yield return null;
        Color[] vertexcolors = new Color[polygonPositions.Length];

        for (int j = 0; j < vertexcolors.Length; j++)
        {
            vertexcolors[j] = IntersectionPolygonColor;
        }
        meshfilter.sharedMesh.Clear();
        meshfilter.sharedMesh.vertices = polygonPositions.toVector3Array();
        meshfilter.sharedMesh.colors = vertexcolors;
        meshfilter.sharedMesh.triangles = (new Triangulator(polygonPositions)).Triangulate();
        meshfilter.sharedMesh.RecalculateNormals();

        meshObject.AddComponent<meshDamageEnemy>();
        var coll = meshObject.AddComponent<PolygonCollider2D>();
        coll.points = polygonPositions;
        coll.isTrigger = true;
          
        StartCoroutine(fadeMeshOverTime(meshfilter));

    }

    IEnumerator fadeMeshOverTime(MeshFilter a)
    {
        meshDamageEnemy m = a.GetComponent<meshDamageEnemy>();
        for (float i = 1; i >= 0; i -= 1f/15)
        {
            m.percentDamageDealt = i* startingPercentDamageToEnemies;
            yield return new WaitForSeconds(0.1f);
             Color scaleColor( Color c)
            {
                return new Color(c.r, c.g, c.b, c.a * i);
            }
            a.sharedMesh.colors = System.Array.ConvertAll<Color, Color>(a.sharedMesh.colors, scaleColor);
            

        }
        if (m.combo > 0)
        {
            BasicScoring.Instance.increaseCombo(m.combo);
        }
        else
        {
            BasicScoring.Instance.resetCombo();
        }
        Destroy(a.gameObject);

    }


    IEnumerator removePointInTime(Vector2 point)
    {
        yield return new WaitForSeconds(timeToDeleteIntersections);
        tomark.Dequeue();
        //Debug.Log(tomark.Count);
    }
    //private void OnDrawGizmos()
    //{
    //    foreach (var v in tomark)
    //    {
    //        Gizmos.DrawSphere(v, 0.1f);
    //    }
    //}
    private bool isLineCollide()
    {
        if (rend.positionCount < 2)
        {
            return false;
        }
        int TotalLines = rend.positionCount - 1;
        myLine[] lines = new myLine[TotalLines];
        if (TotalLines > 1)
        {
            for (int i = 0; i < TotalLines; i++)
            {
                lines[i].StartPoint = rend.GetPosition(i);
                lines[i].EndPoint = rend.GetPosition(i + 1);
            }
        }
        for (int i = 0; i < TotalLines - 1; i++)
        {
            myLine currentLine;
            currentLine.StartPoint = rend.GetPosition(rend.positionCount - 2);
            currentLine.EndPoint = rend.GetPosition(rend.positionCount - 1);
            if (isLinesIntersect(lines[i], currentLine))
            {
                lastIntersection1 = lines[i].StartPoint;
                lastIntersection2 = currentLine.StartPoint;
                intersectIndex = i;
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
public static class MyVector3Extension
{
    public static Vector2[] toVector2Array(this Vector3[] v3)
    {
        return System.Array.ConvertAll<Vector3, Vector2>(v3, getV2fromV3);
    }

    public static Vector2 getV2fromV3(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
    public static Vector3[] toVector3Array(this Vector2[] v2)
    {
        return System.Array.ConvertAll(v2, getV3fromV2);
    }
    public static Vector3 getV3fromV2(Vector2 v2)
    {
        return new Vector3(v2.x, v2.y);
    }
}