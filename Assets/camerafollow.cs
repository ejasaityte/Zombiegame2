using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    //player's Transform component
    public Transform followTransform;

    //sets the camera limits so that it doesn't go off-map
    public float leftLimit=-2.41f;
    public float rightLimit=0.14f;
    public float bottomLimit=-1.2f;
    public float topLimit=3.01f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //sets camera's position to the player's position
        this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);

        //clamps camera's position to the given limits
        transform.position = new Vector3
            (
            Mathf.Clamp(followTransform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(followTransform.position.y, bottomLimit, topLimit),
            this.transform.position.z
            );

    }
}
