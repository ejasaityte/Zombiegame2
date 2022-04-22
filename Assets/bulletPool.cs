using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPool : MonoBehaviour
{
    public static bulletPool SharedInstance;
    public List<GameObject> pooledBullets;
    public GameObject bullet;
    public int poolCounter;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject temp;
        for(int i = 0; i<poolCounter; i++)
        {
            temp = Instantiate(bullet);
            temp.SetActive(false);
            pooledBullets.Add(temp);
        }
    }

    public GameObject getBulletFromPool()
    {
        for(int i=0; i<poolCounter;i++)
        {
            if(!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
