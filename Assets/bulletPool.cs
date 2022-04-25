using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPool : MonoBehaviour
{
    //the main bulletPool object
    public static bulletPool SharedInstance;
    //the list of available bullets
    public List<GameObject> pooledBullets;
    //the object that will be kept in the list
    public GameObject bullet;
    //a counter of how many bullets will be needed, can be set in inspector
    public int poolCounter;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        //initiates the bullet list and fills it with inactive bullets
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
            //goes through the bullet list to find one that isn't currently active
            if(!pooledBullets[i].activeInHierarchy)
            {
                //if one has been found, return it
                return pooledBullets[i];
            }
        }
        //if no inactive bullet has been found, return null
        return null;
    }
}
