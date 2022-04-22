using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletSprite;
    public int shootingSkill = 0;
    private AudioSource bulletSound;
    public float bulletForce = 10f;

    void Start()
    {
        bulletSound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(((Input.GetButtonDown("Fire1")||Input.GetButtonDown("Jump"))) &&(Time.timeScale!=0))
        {
            bulletSound.Play();
            Shoot();
        }
    }

    void Shoot()
    {
        int randomVal = Random.Range(45+shootingSkill, 135-shootingSkill);
        Vector3 spread = new Vector3(0, 0, randomVal-90);
        var rotation = firePoint.rotation;
        rotation *= Quaternion.Euler(0, 0, 90);
        GameObject bullet = Instantiate(bulletSprite, firePoint.position, Quaternion.Euler(rotation.eulerAngles + spread));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
