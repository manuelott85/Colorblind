using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float moveForce = 10;
    private bool moveRight = true;
    public float maxMovementSpan = 50;
    private float currentMovementSpan = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {       
        if (currentMovementSpan >= maxMovementSpan && currentMovementSpan > 0)
        {
            moveRight = false;
        }
        else if (currentMovementSpan <= -maxMovementSpan&& currentMovementSpan<0)
        {
            moveRight = true;
        }

        if (moveRight == true)
        {
            transform.Translate(new Vector3(moveForce, 0, 0));
            currentMovementSpan += moveForce;
        }

        if (moveRight == false)
        {
            transform.Translate(new Vector3(-moveForce, 0, 0));
            currentMovementSpan -= moveForce;
        }
    }
}
