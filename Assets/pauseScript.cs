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
        pauseScreen = GameObject.Find("pauseScreen");
        pauseScreen.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((Time.timeScale == 1) && (paused == false))
            {
                pause();
            }
            else if ((Time.timeScale == 0) && (paused == true))
            {
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
