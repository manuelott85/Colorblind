using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    Rigidbody2D myobj;
    public float moveForce = 20;
    public float jumpForce = 5;
    public float minVelocity = 0.5f;
    public string forceValueRL;

    //for Jumping 
    public string forceButtonA;

    private bool m_isAxisInUse = false;
    // Use this for initialization
    void Start()

    {


    }

    // Update is called once per frame
    void Update()
    {
        myobj = GetComponent<Rigidbody2D>();
       // Debug.Log(myobj.velocity.y);
        if (forceValueRL == ""  || forceButtonA =="")
        {
            return;
        }
        //Moving Left and Right
        if (Input.GetAxis(forceValueRL) != 0)
        {

            float forceValueNum = Input.GetAxis(forceValueRL);

            myobj.AddForce(new Vector2(forceValueNum * moveForce, 0));

           // Debug.Log("MOVE RIGHT LEFT");
        }

        //Moving Left and Right
        if (Input.GetButton(forceButtonA))
        {
     

                if (myobj.velocity.y < minVelocity && myobj.velocity.y > -minVelocity)
                {

                    myobj.AddForce(new Vector2(0,  jumpForce),ForceMode2D.Impulse);

                    //Debug.Log("jump");

                    m_isAxisInUse = true;
                }

        }

    }


}
