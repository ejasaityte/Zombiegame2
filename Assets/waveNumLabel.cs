using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveNumLabel : MonoBehaviour
{
    private int waveNum = 1;
    public enemySpawning spawner;
    public Text txt;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject manager = GameObject.Find("enemyManager");
        spawner = manager.GetComponent<enemySpawning>();
        txt = GetComponent<Text>();
        txt.text = "Wave: "+waveNum;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        waveNum = spawner.currentWave;
        txt.text = "Wave: " + waveNum;
    }
}
