using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieFollow : MonoBehaviour
{
    //gets sound effect for when an attack hits and misses
    public AudioSource attackHit, attackMiss;
    public Transform target;
    public float speed = 1.2f;
    //the zombie's sprite renderer component
    private SpriteRenderer renderer;
    //counter of currently runnning Flash coroutines
    private int runningFlash = 0;
    public int damage = 30;

    //the available states for the zombie, following and attacking
    enum State
    {
        state_follow,
        state_attack
    };
    //the state the zombie is currently in
    private State state_;

    void Start()
    {
        //sets the starting state of the zombie to follow the player
        state_ = State.state_follow;
        renderer = GetComponent<SpriteRenderer>();
        if (target == null)
        {
            //sets the target to be the player character
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        switch(state_)
        {
            //if the zombie is currently in the follow state, set it to go towards player
            case State.state_follow:
                //calculates distance between the zombie and the player
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance > 0.3)
                {
                    //if the distance is less than needed for attack, go towards player
                    transform.LookAt(target.position);
                    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                }
                else
                {
                    //if zombie is close enough to attack, change state to attacking
                    state_ = State.state_attack;
                }
                break;
            //if zombie is currently attacking
            case State.state_attack:
                //if zombie is within attack range and is not currently attacking, run Flash coroutine
                if (runningFlash == 0) StartCoroutine(Flash());
                break;
        }
    }

    IEnumerator Flash()
    {
        //increments the counter of currently running attack coroutines
        runningFlash++;
        //changes zombie sprite's colour to white and waits for a short delay
        renderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.15f);
        //recalculates the distance to check if player moved away in time
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 0.3)
        {
            //if player still in range, play the hit sound effect and subtract damage from the player's health
            attackHit.Play();
            target.GetComponent<playerHealth>().AdjustCurrentHealth(damage);
        }
        else attackMiss.Play();

        //resets the sprite's colour to the original
        renderer.color = new Color(0.7270476f, 0.9056604f, 0.6365255f, 1f);
        //short delay before attack is possible again
        yield return new WaitForSeconds(0.1f);
        //decrement counter to allow for another Flash coroutine
        runningFlash--;
        //return state to following
        state_ = State.state_follow;
    }
}
