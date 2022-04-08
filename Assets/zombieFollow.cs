using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 1.2f;
    private SpriteRenderer renderer;
    private int runningFlash = 0;
    public int damage = 30;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > 0.3)
        {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);


            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
        {
            if(runningFlash==0) StartCoroutine(Flash(distance));
        }

    }
    IEnumerator Flash(float distance)
    {
        runningFlash++;
        renderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.8f);
        if (distance <= 0.3)
        {
            target.GetComponent<playerHealth>().AdjustCurrentHealth(damage);
        }
        renderer.color = new Color(0.7270476f, 0.9056604f, 0.6365255f, 1f);
        yield return new WaitForSeconds(1f);
        runningFlash--;
    }
}
