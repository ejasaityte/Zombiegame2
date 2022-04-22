using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveNumLabel : MonoBehaviour
{
    private int waveNum = 1;
    public enemySpawning spawner;
    public Text txt;
    void Awake()
    {
        //finds the game controller
        GameObject manager = GameObject.Find("enemyManager");
        spawner = manager.GetComponent<enemySpawning>();

        //gets the text component of the wave number label
        txt = GetComponent<Text>();

        //starting text should read Wave: 1
        txt.text = "Wave: "+waveNum;
    }

    void FixedUpdate()
    {
        //periodically gets the current wave number from the spawning script
        waveNum = spawner.currentWave;
        txt.text = "Wave: " + waveNum;
    }
}
