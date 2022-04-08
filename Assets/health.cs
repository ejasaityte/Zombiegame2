using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public int curHealth = 40;
    private SpriteRenderer renderer;
    private int runningFlash = 0;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void AdjustCurrentHealth(int adj)
    {
        curHealth -= adj;
        if (runningFlash == 0) StartCoroutine(Flash());
        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator Flash()
    {
        runningFlash++;
        renderer.color = new Color(0.9058824f, 0.7353123f, 0.6352941f, 1f);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(0.7270476f, 0.9056604f, 0.6365255f, 1f);
        yield return new WaitForSeconds(0.1f);
        runningFlash--;
    }
}
