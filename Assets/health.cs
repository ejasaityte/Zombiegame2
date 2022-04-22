using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public int curHealth = 40;
    private SpriteRenderer renderer;

    //counter of currently running Flash coroutines
    private int runningFlash = 0;

    void Start()
    {
        //gets the renderer component of the zombie sprite
        renderer = GetComponent<SpriteRenderer>();
    }
    public void AdjustCurrentHealth(int adj)
    {
        //subtracts damage from zombie's health
        curHealth -= adj;

        //if there are no currently running Flash subroutines, start a new one
        if (runningFlash == 0) StartCoroutine(Flash());

        //if zombie's health is empty, destroy's the zombie object
        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator Flash()
    {
        //increments counter to only allow one instance of coroutine
        runningFlash++;

        //changes the zombie sprite's colour to white
        renderer.color = new Color(0.9058824f, 0.7353123f, 0.6352941f, 1f);
        yield return new WaitForSeconds(0.1f);

        //returns to sprite's original colour after short delay
        renderer.color = new Color(0.7270476f, 0.9056604f, 0.6365255f, 1f);
        yield return new WaitForSeconds(0.1f);

        //decrements counter
        runningFlash--;
    }
}
