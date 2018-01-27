using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDeadzone : MonoBehaviour {

    public float campSpeed = 1;

    private Transform playerA, playerB;
    private bool playerAIsInside = true, playerBIsInside = true;

	// Use this for initialization
	void Start () {
        playerA = GameManager.instance.playerA;
        playerB = GameManager.instance.playerB;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(!playerAIsInside || !playerBIsInside)
        {
            moveCam();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "PlayerA")
        {
            playerAIsInside = false;
        }
        if (collision.name == "PlayerB")
        {
            playerBIsInside = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerA")
        {
            playerAIsInside = true;
        }
        if (collision.name == "PlayerB")
        {
            playerBIsInside = true;
        }
    }

    private void moveCam()
    {
        Vector2 centerPointBetweenPlayers = (playerA.position + playerB.position) * 0.5f;
        transform.position = Vector2.Lerp(transform.position, centerPointBetweenPlayers, campSpeed);
        //transform.position = centerPointBetweenPlayers;
    }
}
