using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMenu : MonoBehaviour
{
    public void goToMenu()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
        SceneManager.UnloadScene(currentIndex);
        Time.timeScale = 1;
    }
}
