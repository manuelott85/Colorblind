using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    Rigidbody2D myobj;
    public float moveForce = 10;
    private bool moveRight = true;
    private bool moveLeft = false;
    public float maxMovementSpan = 50;
    private float currentMovementSpan = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        myobj = GetComponent<Rigidbody2D>();

 
       
            if ( currentMovementSpan >= maxMovementSpan && currentMovementSpan >0)
            {
                moveRight = false;
                moveLeft = true;
  
            }

            else if (currentMovementSpan <= -maxMovementSpan&& currentMovementSpan<0)
            {
            moveRight = true;
            moveLeft = false;
            }

            if (moveRight == true)
            {
                myobj.AddForce(new Vector2(moveForce, 0));
                currentMovementSpan += moveForce;
                // Debug.Log("MOVE RIGHT LEFT");
            }
            if (moveRight == false)
            {
                myobj.AddForce(new Vector2(-moveForce, 0));
                currentMovementSpan -= moveForce;
            } 
          



    }


}
