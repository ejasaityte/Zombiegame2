using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int curHealth = 300;
    public int maxHealth = 300;
    public void AdjustCurrentHealth(int adj)
    {
        if (curHealth - adj < 0)
        {
            curHealth = 0;
            SceneManager.LoadScene(3);
        }
        else curHealth -= adj;


    }
}
