using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BasicScoring : MonoBehaviour
{
    public int Highscore = 0;
    public int CurrentScore = 0;
    public float timeLeft = 0;
    public float totalTime = 30;
    TextMeshProUGUI textGUI;
    float startTime;
    bool clockStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("highscore"))
        {
            Highscore = PlayerPrefs.GetInt("highscore");
        }
        textGUI.text = "Highscore: " + Highscore + "\nCurrent Score: 0\nTime: 0";
        StartClock();
    }

    // Update is called once per frame
    void Update()
    {
        if (clockStarted)
        {
            if (CurrentScore > Highscore)
            {
                Highscore = CurrentScore;
            }

            timeLeft = totalTime - (Time.time - startTime);
            if (timeLeft <= 0)
            {
                textGUI.text = "Highscore: " + Highscore +
                "\nCurrent Score: " + CurrentScore +
                "\nTime: " + 0;
                StopClock();
            }
            textGUI.text = "Highscore: " + Highscore + 
                            "\nCurrent Score: " + CurrentScore + 
                            "\nTime: " + timeLeft;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentScore++;
        }
    }


    public void StartClock()
    {
        clockStarted = true;
        timeLeft = totalTime;
        startTime = Time.time;
    }
    public void StopClock()
    {
        clockStarted = false;
        updateHighscore();
        SceneManager.LoadScene(0);
    }
    public void updateHighscore()
    {
        PlayerPrefs.SetInt("highscore", Highscore);
    }
    [ContextMenu("Reset Highscore")]
    public void resetHighscore()
    {
        PlayerPrefs.SetInt("highscore", 0);
    }
}
