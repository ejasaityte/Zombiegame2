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

        //destroy's the bullet's object after short delay
        Destroy(gameObject, 0.2f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if the bullet collides with an enemy, subtract damage from the enemy's health and destroy bullet
            collision.gameObject.GetComponent<health>().AdjustCurrentHealth(damage);
            Destroy(gameObject);
        }
        
    }

}
