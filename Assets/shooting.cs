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
    private bool bulletShot = false;

    void Start()
    {
        //gets the bullet shoot sound
        bulletSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(((Input.GetButtonDown("Fire1")||Input.GetButtonDown("Jump"))) &&(Time.timeScale!=0))
        {
            //if player presses their mouse button or space when the game isn't paused, a bullet is fired
            if(!bulletShot) StartCoroutine(shootBulletDelayed());
        }
    }

    IEnumerator shootBulletDelayed()
    {
        bulletShot = true;
        bulletSound.Play();
        Shoot();
        yield return new WaitForSeconds(0.25f);
        bulletShot = false;
    }

    void Shoot()
    {
        //random angle for bullet spread, can be 90 degrees at most with maximum shooting skill
        int randomVal = Random.Range(45+shootingSkill, 135-shootingSkill);
        Vector3 spread = new Vector3(0, 0, randomVal-90);

        //gets rotation of point where bullet will come from, rotates it to offset initial sprite position
        var rotation = firePoint.rotation;
        rotation *= Quaternion.Euler(0, 0, 90);

        //creates new bullet at firepoint
        GameObject bullet = bulletPool.SharedInstance.getBulletFromPool();
        if(bullet!=null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.Euler(rotation.eulerAngles + spread);
            bullet.SetActive(true);
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
