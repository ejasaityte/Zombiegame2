using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 move;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        //gets provided movement input
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        //gets mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        //moves player in given direction
        rb.MovePosition(rb.position + move * movementSpeed * Time.fixedDeltaTime);

        //rotates player to face mouse position
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

}
    