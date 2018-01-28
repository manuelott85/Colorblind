using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour {

    Rigidbody2D rbody;
    Vector2 velocity;
    float deadzone = 2;

    public RuntimeAnimatorController walking;
    public RuntimeAnimatorController jumping;
    public RuntimeAnimatorController death;

    // Use this for initialization
    void Start () {
        rbody = transform.parent.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        velocity = rbody.velocity;
        if (GameManager.instance.getIsAlive(transform.parent.GetComponent<dieingSystem>().isPlayerA))
        {
            if (velocity.x == 0 && velocity.y == 0)
                GetComponent<Animator>().enabled = false;
            else
            {
                GetComponent<Animator>().enabled = true;
                if (velocity.x > deadzone)
                    GetComponent<SpriteRenderer>().flipX = true;
                if (velocity.x < -deadzone)
                    GetComponent<SpriteRenderer>().flipX = false;
                if (velocity.y > deadzone || velocity.y < -deadzone)
                    GetComponent<Animator>().runtimeAnimatorController = jumping;
                else
                    GetComponent<Animator>().runtimeAnimatorController = walking;
            }
        }
        else
            GetComponent<Animator>().runtimeAnimatorController = death;
    }
}
