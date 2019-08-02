using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject togglePause;

    void Start()
    {
        togglePause.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseTheGame();
        }
    }
    public void PauseTheGame()
    {
        togglePause.SetActive(!togglePause.activeSelf);

        if (togglePause.activeSelf == true)
        {
            Debug.Log("GAME PAUSED");
            Time.timeScale = 0;
        } else
        {
            Debug.Log("GAME NOT PAUSED");
            Time.timeScale = 1;
        }        
    }
}
