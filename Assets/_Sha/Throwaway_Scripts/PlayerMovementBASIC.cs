using UnityEngine;
using Sha_Throwaways;

namespace Sha_Throwaways
{
    public class PlayerMovementBASIC : MonoBehaviour
    {
        public float speed;
        Rigidbody2D rb;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = transform.up * speed;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                rb.velocity = -transform.up * speed;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = -transform.right * speed;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = transform.right * speed;
            }
        }
    }
}
