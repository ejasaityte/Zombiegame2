using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    int damage=10;
    //private AudioSource shootingSound;

    void Awake()
    {
        //shootingSound = GetComponent<AudioSource>();
        //shootingSound.Play();
    }

    void Update()
    {
        //destroys the bullet's object after short delay
        StartCoroutine(destroyDelay());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if the bullet collides with an enemy, subtract damage from the enemy's health and destroy bullet
            collision.gameObject.GetComponent<health>().AdjustCurrentHealth(damage);
            gameObject.SetActive(false);
        }
        
    }

    IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

}
