﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class healthManager : MonoBehaviour
{
    public float currentHealthPercent = 1;
    public float remainingHearts = 3;

    public void Damage(float percentAmount)
    {
        if (currentHealthPercent - percentAmount <= 0)
        {
            if (remainingHearts == 0)
            {
                Die();
            }
            else
            {
                currentHealthPercent = 1;
                remainingHearts--;
                heartbar.Instance.DepleteHeart();
            }

        }
        else
        {
            currentHealthPercent -= percentAmount;
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Damage(.51f);
        //}
    }
}