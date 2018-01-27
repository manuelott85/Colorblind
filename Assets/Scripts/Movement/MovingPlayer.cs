using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    Rigidbody2D myobj;
    public float moveForce = 20;
    public float jumpForce = 5;
    public float minVelocity = 0.0005f;
    public string forceValueRL;
    public string forceValueUD;
    private bool m_isAxisInUse = false;
    // Use this for initialization
    void Start()

    {


    }

    // Update is called once per frame
    void Update()
    {
        myobj = GetComponent<Rigidbody2D>();
        Debug.Log(myobj.velocity.y);
        if (forceValueRL == "" || forceValueUD == "")
        {
            return;
        }
        //Moving Left and Right
        if (Input.GetAxis(forceValueRL) != 0)
        {



            float forceValueNum = Input.GetAxis(forceValueRL);

            myobj.AddForce(new Vector2(forceValueNum * moveForce, 0));

            Debug.Log("MOVE RIGHT LEFT");
        }

        //Moving Left and Right
        if (Input.GetAxis(forceValueUD) != 0)
        {
            if (m_isAxisInUse == false)
            {


                if (myobj.velocity.y < minVelocity && myobj.velocity.y > -minVelocity)
                {

                    // Call your event function here.
                    float forceValueNum = Input.GetAxis(forceValueUD);

                    myobj.AddForce(new Vector2(0, forceValueNum * jumpForce),ForceMode2D.Impulse);

                    Debug.Log("MOVE UP DOWN");

                    m_isAxisInUse = true;
                }


            }
        }
        else if (Input.GetAxis(forceValueUD) == 0)
        {
            m_isAxisInUse = false;
        }
    }


}
