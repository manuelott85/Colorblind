using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateParent : MonoBehaviour {

    [Tooltip("How much the object should rotate in degree per frame")]
    [Range(0,360)]
    public float rotationValue = 20;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0, rotationValue));
    }
}
