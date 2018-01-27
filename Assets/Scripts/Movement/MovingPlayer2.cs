using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer2 : MonoBehaviour
{
    Rigidbody2D myobj;
    public float force = 20;

    // Use this for initialization
    void Start()

    {


    }

    // Update is called once per frame
    void Update()

    {
        //Moving Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(-force, 0));
            Debug.Log("MOVE Left");
        }
        //Moving Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(force, 0));
            Debug.Log("MOVE Right");
        }
        //Moving Up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(0, force));
            Debug.Log("MOVE Up");
        }
        //Moving down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(0, -force));
            Debug.Log("MOVE Down");
        }
    }
}
