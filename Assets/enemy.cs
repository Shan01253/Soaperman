using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public void kill()
    {
        gameObject.SetActive(false);
    }

}
