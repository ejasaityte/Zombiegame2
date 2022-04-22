using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseScript : MonoBehaviour
{
    public GameObject pauseScreen;
    private bool paused = false;

    void Start()
    {
        //finds the pause screen panel and sets it to inactive just in case
        pauseScreen = GameObject.Find("pauseScreen");
        pauseScreen.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((Time.timeScale == 1) && (paused == false))
            {
                //if escape key is detected, the reward screen isn't on, and the game is unpaused, pauses the game
                pause();
            }
            else if ((Time.timeScale == 0) && (paused == true))
            {
                //if the game is currently paused, unpause
                unpause();
            }
        }
    }

    void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    void unpause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}
