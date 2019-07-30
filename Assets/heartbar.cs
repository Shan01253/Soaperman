using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartbar : MonoBehaviour
{
    int i = 0;
    public static heartbar Instance;
    private void Start()
    {
        Instance = this;
    }
    public void DepleteHeart()
    {
        if (i == 3)
        {
            return;
        }
        transform.GetChild(i++).gameObject.SetActive(false);
    }
    public void ReplenishHeart()
    {
        if (i == 0)
        {
            return;
        }
        transform.GetChild(i--).gameObject.SetActive(true);
    }
}
