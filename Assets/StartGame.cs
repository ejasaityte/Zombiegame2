using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //a boolean letting the game know if the game needs to be loaded from save
    public static bool load = false;
    public void startGame()
    {
        //loads the game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void loadGame()
    {
        load = true;
        //loads the game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitGame()
    {
        //exits application
        Application.Quit();
    }
}
