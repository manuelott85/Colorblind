using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour {
    
    private Vector2 velocity;
    private float deadzone = 2;

    [Tooltip("link/path to the walking animation")]
    public RuntimeAnimatorController walking;
    [Tooltip("link/path to the jumping animation")]
    public RuntimeAnimatorController jumping;
    [Tooltip("link/path to the death animation")]
    public RuntimeAnimatorController death;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;  // get the velocity

        // if the player is not dead
        if (GameManager.instance.getIsAlive(transform.parent.GetComponent<dieingSystem>().isPlayerA))
        {
            // In case the player is not moving: disable the animator
            if (velocity.x == 0 && velocity.y == 0)
                GetComponent<Animator>().enabled = false;
            // If the character is moving activate the animator
            else
            {
                GetComponent<Animator>().enabled = true;

                // Determine if the player is moving to the right or left and flip the animations accordingly
                if (velocity.x > deadzone)
                    GetComponent<SpriteRenderer>().flipX = true;
                if (velocity.x < -deadzone)
                    GetComponent<SpriteRenderer>().flipX = false;

                // Update the animator if the player is jumping or not
                if (velocity.y > deadzone || velocity.y < -deadzone)
                    GetComponent<Animator>().runtimeAnimatorController = jumping;
                else
                    GetComponent<Animator>().runtimeAnimatorController = walking;
            }
        }
        // if the player is dead, play the death animation
        else
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().runtimeAnimatorController = death;
        }
    }
}
