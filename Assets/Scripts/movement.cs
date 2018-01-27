using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.A))
        {
            rbody.AddForce(new Vector2(-50, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rbody.AddForce(new Vector2(50, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            rbody.AddForce(new Vector2(0, 50));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rbody.AddForce(new Vector2(0, -50));
        }
    }
}
