using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBirdEnemy : MonoBehaviour
{

    public float rotationValue = 6.5f;
    private float currentRotationValue = 0;
    public float maxRotationRightSpan = 50;
    public float maxRotationLeftSpan = -50;
    bool toRightRotate = true;

    public float moveValue = 10;
    private bool moveRight = true;

    private bool moveUp = true;
   
    public float maXMovementSpan = 50;
    private float currentXMovementSpan = 0;



    Rigidbody2D myobj;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (currentRotationValue > maxRotationRightSpan)
        {
            toRightRotate = false;

            //Debug.Log(transform.Find("birdie_0").GetComponent<SpriteRenderer>().flipX.ToString());

            if (transform.Find("birdie_0") != null)
            {
                transform.Find("birdie_0").GetComponent<SpriteRenderer>().flipX = true;
            }

            else if (transform.Find("birdie2_0") != null)

            {
                transform.Find("birdie2_0").GetComponent<SpriteRenderer>().flipX = true;
            }
    
        }
        else if (currentRotationValue < maxRotationLeftSpan)
        {
            toRightRotate = true;
            //Debug.Log(transform.Find("birdie_0").GetComponent<SpriteRenderer>().flipX.ToString());


            if (transform.Find("birdie_0") != null)
            {
                transform.Find("birdie_0").GetComponent<SpriteRenderer>().flipX = false;
            }

            else if (transform.Find("birdie2_0") != null)
            {
                transform.Find("birdie2_0").GetComponent<SpriteRenderer>().flipX = false;

            }
                
          

        }

        if (toRightRotate == true)
        {
            transform.Rotate(new Vector3(0, 0, rotationValue));
            currentRotationValue += rotationValue;

        }

        else if (toRightRotate == false)
        {
            transform.Rotate(new Vector3(0, 0, -rotationValue));
            currentRotationValue -= rotationValue;

        }



        // move right left 
        if (currentXMovementSpan >= maXMovementSpan && currentXMovementSpan > 0)
        {
            moveRight = false;
            


        }

        else if (currentXMovementSpan <= -maXMovementSpan && currentXMovementSpan < 0)
        {
            moveRight = true;
        }


        if (moveRight == true)
        {
          //  transform.Translate(new Vector3(0, moveValue, 0));
           // currentXMovementSpan += moveValue;
            // Debug.Log("MOVE RIGHT LEFT");
        }
        if (moveRight == false)
        {
          //  transform.Translate(new Vector3(0, -moveValue, 0));
           // currentXMovementSpan -= moveValue;
        }

    }
}
