using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMenu : MonoBehaviour
{
    public void goToMenu()
    {
        //gets current scene's index
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //loads menu scene and unloads current scene
        SceneManager.LoadScene(0);
        SceneManager.UnloadScene(currentIndex);

        //unpauses if the game was in pause screen
        Time.timeScale = 1;
    }
}
