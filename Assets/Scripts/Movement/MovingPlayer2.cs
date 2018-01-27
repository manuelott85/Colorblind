using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer2 : MonoBehaviour
{
    Rigidbody2D myobj;
    public float force = 20;
    public string forceValueRL;
    public string forceValueUD;
    // Use this for initialization
    void Start()

    {


    }

    // Update is called once per frame
    void Update()

    {
        //Moving Left and Right
        if (Input.GetAxis(forceValueRL) != 0)
        {

            myobj = GetComponent<Rigidbody2D>();

            float forceValueNum = Input.GetAxis(forceValueRL);

            myobj.AddForce(new Vector2(forceValueNum*force, 0));

            Debug.Log("MOVE RIGHT LEFT");
        }

        //Moving Left and Right
        if (Input.GetAxis(forceValueUD) != 0)
        {

            myobj = GetComponent<Rigidbody2D>();

            float forceValueNum = Input.GetAxis(forceValueUD);

            myobj.AddForce(new Vector2(0, forceValueNum*force));

            Debug.Log("MOVE UP DOWN");
        }

    }
}
