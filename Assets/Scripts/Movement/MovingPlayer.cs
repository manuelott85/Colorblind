using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour {
    Rigidbody2D myobj;
    public  float force = 20;

    // Use this for initialization
    void Start ()

    {
        
 
	}
	
	// Update is called once per frame
	void Update ()

    {
        //Moving Left
        if (Input.GetKey(KeyCode.A))
        {
            myobj= GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(- force, 0));
            Debug.Log("MOVE Left");
        }
        //Moving Right
        if (Input.GetKey(KeyCode.S))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(force, 0));
            Debug.Log("MOVE Right");
        }
         //Moving Up
        if (Input.GetKey(KeyCode.W))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(0, force));
            Debug.Log("MOVE Up");
        }
        //Moving down
        if (Input.GetKey(KeyCode.X))
        {
            myobj = GetComponent<Rigidbody2D>();
            myobj.AddForce(new Vector2(0, -force));
            Debug.Log("MOVE Down");
        }
    }
}
