using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableContinue : MonoBehaviour
{
    public Button ContinueButton;
    
    void Start()
    {
        ContinueButton = GetComponent<Button>();
        if (PlayerPrefs.GetInt("currentWave", 1) == 1)
        {
            ContinueButton.interactable = false;
        }
    }
}
