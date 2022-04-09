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
        Destroy(gameObject, 0.2f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<health>().AdjustCurrentHealth(damage);
            Destroy(gameObject);
        }
        
    }

}
