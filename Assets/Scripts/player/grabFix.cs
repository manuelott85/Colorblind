using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabFix : MonoBehaviour {

    public float traceLenght = 0.5f;
    public float characterHeight = 0.5f;
    public float characterWidth = 0.4f;

    private bool isCollidingRight = false;
    private bool isCollidingLeft = false;
    private bool isCollidingUp = false;

    public bool getIsCollidingRight() { return isCollidingRight; }
    public bool getIsCollidingLeft() { return isCollidingLeft; }
    public bool getIsCollidingUp() { return isCollidingUp; }

    RaycastHit2D[] colRight1, colRight2, colRight3;
    RaycastHit2D[] colLeft1, colLeft2, colLeft3;
    RaycastHit2D[] colUp1, colUp2;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        isCollidingRight = false;
        isCollidingLeft = false;
        isCollidingUp = false;
        //Debug.DrawLine(transform.position, transform.position + new Vector3(characterWidth, traceLenght * 1.5f, 0));
        colRight1 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(traceLenght, 0, 0));
        colRight2 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(traceLenght, characterHeight, 0));
        colRight3 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(traceLenght, -characterHeight, 0));
        colLeft1 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-traceLenght, 0, 0));
        colLeft2 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-traceLenght, characterHeight, 0));
        colLeft3 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-traceLenght, -characterHeight, 0));
        colUp1 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(characterWidth, traceLenght * 1.5f, 0));
        colUp2 = Physics2D.LinecastAll(transform.position, transform.position + new Vector3(-characterWidth, traceLenght * 1.5f, 0));
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
