using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    //keeps track of player's current and maximum health
    public int curHealth = 300;
    public int maxHealth = 300;
    public void AdjustCurrentHealth(int adj)
    {
        //if player has reached 0 health, it unsets all saved attributes and goes to game loss screen
        if (curHealth - adj <= 0)
        {
            curHealth = 0;
            PlayerPrefs.SetInt("currentWave", 1);
            PlayerPrefs.SetInt("maxEnemyHealth", 40);
            PlayerPrefs.SetInt("enemyDamage", 30);
            PlayerPrefs.SetFloat("enemySpeed", 1.2f);
            PlayerPrefs.SetFloat("movementSpeed", 1f);
            PlayerPrefs.SetInt("curHealth", 300);
            PlayerPrefs.SetInt("maxHealth", 300);
            PlayerPrefs.SetInt("shootingSkill", 0);
            SceneManager.LoadScene(3);
        }
        else curHealth -= adj;


    }
}
