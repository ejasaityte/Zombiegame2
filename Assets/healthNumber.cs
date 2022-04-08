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
    // Start is called before the first frame update
    void Awake()
    {
        GameObject player = GameObject.Find("player");
        playerHealth = player.GetComponent<playerHealth>();
        curHealth = playerHealth.curHealth;
        maxHealth = playerHealth.maxHealth;
        txt = GetComponent<Text>();
        txt.text = curHealth+ "/" + maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curHealth = playerHealth.curHealth;
        maxHealth = playerHealth.maxHealth;
        txt = GetComponent<Text>();
        txt.text = curHealth+ "/" + maxHealth;
    }
}
