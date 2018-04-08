using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabFix : MonoBehaviour {

    [Tooltip("Length of the line trace to check for collision around the player")]
    public float traceLength = 0.5f;
    [Tooltip("The height of the player")]
    public float characterHeight = 0.5f;
    [Tooltip("The width of the player")]
    public float characterWidth = 0.4f;

    private bool isCollidingRight = false;
    private bool isCollidingLeft = false;
    private bool isCollidingUp = false;

    private RaycastHit2D[] colRight1, colRight2, colRight3;
    private RaycastHit2D[] colLeft1, colLeft2, colLeft3;
    private RaycastHit2D[] colUp1, colUp2;

    // Returns if there is a collision
    // Is used by the MovingPlayer.cs to disable the input while colliding
    public bool getIsCollidingRight() { return isCollidingRight; }
    public bool getIsCollidingLeft() { return isCollidingLeft; }
    public bool getIsCollidingUp() { return isCollidingUp; }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // reset the variables each frame
        isCollidingRight = false;
        isCollidingLeft = false;
        isCollidingUp = false;

        //Debug.DrawLine(transform.position, transform.position + new Vector3(characterWidth, traceLength * 1.5f, 0));

        // Line trace for objects at the feet (to left and rights), at the hip (to left an right) and at the head (left, right and two times up for each ear)
        colRight1 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(traceLength, 0, 0));
        colRight2 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(traceLength, characterHeight, 0));
        colRight3 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(traceLength, -characterHeight, 0));
        colLeft1 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-traceLength, 0, 0));
        colLeft2 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-traceLength, characterHeight, 0));
        colLeft3 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-traceLength, -characterHeight, 0));
        colUp1 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(characterWidth, traceLength * 1.5f, 0));
        colUp2 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-characterWidth, traceLength * 1.5f, 0));

        // Check each line trace for a collision. If there is one, update the corresponding variable
        foreach (RaycastHit2D element in colRight1)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingRight = true;
        }
        foreach (RaycastHit2D element in colRight2)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingRight = true;
        }
        foreach (RaycastHit2D element in colRight3)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingRight = true;
        }
        foreach (RaycastHit2D element in colLeft1)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingLeft = true;
        }
        foreach (RaycastHit2D element in colLeft2)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingLeft = true;
        }
        foreach (RaycastHit2D element in colLeft3)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingLeft = true;
        }
        foreach (RaycastHit2D element in colUp1)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingUp = true;
        }
        foreach (RaycastHit2D element in colUp2)
        {
            if (element.transform.gameObject.layer == 0 || element.transform.gameObject.layer == 9)
                isCollidingUp = true;
        }
    }
}
