using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulmnEnemy : MonoBehaviour
{

    public float rotationValue = 6.5f;
    private float currentRotationValue = 0;
    public float maxRotationRightSpan = 50;
    public float maxRotationLeftSpan = -50;
    bool toRightRotate = true ;

  

    Rigidbody2D myobj;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (currentRotationValue > maxRotationRightSpan )
        {
            toRightRotate = false;
          
        }
        else if (currentRotationValue <maxRotationLeftSpan )
        {
            toRightRotate = true;
            
        }

        if ( toRightRotate ==true)
        {
            transform.Rotate(new Vector3(0, 0, rotationValue));
            currentRotationValue += rotationValue;

        }

       else  if (toRightRotate == false)
        {
            transform.Rotate(new Vector3(0, 0, -rotationValue));
            currentRotationValue -= rotationValue;

        }



    }
}
