using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthNumber : MonoBehaviour
{
    public playerHealth playerHealth;
    public Text txt;
    private int curHealth;
    private int maxHealth;

    void Awake()
    {
        //finds player and gets their health component's attributes
        GameObject player = GameObject.Find("player");
        playerHealth = player.GetComponent<playerHealth>();
        curHealth = playerHealth.curHealth;
        maxHealth = playerHealth.maxHealth;

        //gets health counter label's text and changes it to show player's health
        txt = GetComponent<Text>();
        txt.text = curHealth+ "/" + maxHealth;
    }


    void FixedUpdate()
    {
        //peridically gets player's current and maximum health to display
        curHealth = playerHealth.curHealth;
        maxHealth = playerHealth.maxHealth;
        txt = GetComponent<Text>();
        txt.text = curHealth+ "/" + maxHealth;
    }
}
