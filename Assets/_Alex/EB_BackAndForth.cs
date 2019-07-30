using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_BackAndForth : MonoBehaviour
{
    public enum Direction {
        Up, Down, Left, Right
    }

    public Direction EnemyDirection;
    public int PauseInterval;
    public int WalkInterval;
    public float EnemySpeed;

    private Direction InternalState;
    private Rigidbody2D rb;
    //private bool ChangeState;
    //private bool IsStationary;
 
    private IEnumerator Countdown() {
        yield return new WaitForSeconds(WalkInterval);
        Debug.Log("Pause interval reached");
        //if (!IsStationary) {
            // IsStationary = true;
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(PauseInterval);
        Debug.Log("Walk interval reached");
        //}

        //ChangeState = true;
        SwapDirection();
        SetVelocity();
        StartCoroutine(Countdown());
    }

    private void SwapDirection() {
        switch (InternalState) {
            case Direction.Up:
                InternalState = Direction.Down;
                break;
            case Direction.Down:
                InternalState = Direction.Up;
                break;
            case Direction.Left:
                InternalState = Direction.Right;
                break;
            case Direction.Right:
                InternalState = Direction.Left;
                break;
            default:
                Debug.Log("ERROR: invalid InternalState");
                break;
        }
    }

    private void SetVelocity() {
       switch (InternalState) {
           case Direction.Up:
               rb.velocity = new Vector2(0.0f, -EnemySpeed);
               break;
           case Direction.Down:
               rb.velocity = new Vector2(0.0f, EnemySpeed);
               break;
           case Direction.Left:
               rb.velocity = new Vector2(-EnemySpeed, 0.0f);
               break;
           case Direction.Right:
               rb.velocity = new Vector2(EnemySpeed, 0.0f);
               break;
           default:
               Debug.Log("ERROR: invalid InternalState");
               break;
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        InternalState = EnemyDirection;
        //ChangeState = false;
        //IsStationary = false;

        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
