using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDeadzone : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This component moves the camera to a target location based on the position of each living player smoothly";

    [Tooltip("Moving Speed of the camera")]
    [Range(0.001f, 0.4f)]
    public float camSpeed = 0.09f;
    [Tooltip("Flying altitude of the camera, measured on top of the average character heights")]
    [Range(-5, 5)]
    public float cameraHeight = 0;

    private Transform playerA, playerB;
    private bool playerAIsInside = false, playerBIsInside = false;  // Init with false, in case the CamDeadzone is deactivated (TriggerBox2D)

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
        // Move the camera if on of the players is not inside the CamDeadzone (TriggerBox2D)
        if(!playerAIsInside || !playerBIsInside)
        {
            moveCam();
        }
    }

    // Update the variable in case a player moves out the deadzone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerA")
        {
            playerAIsInside = false;
        }
        if (collision.name == "PlayerB")
        {
            playerBIsInside = false;
        }
    }

    // Update the variable in case the camera was able to capture/include the missing player again
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

    // Calculates a target location for the camera and move towards it smoothly
    private void moveCam()
    {
        Vector2 centerPointBetweenPlayers = new Vector2(0,0);   // Init the target location

        // In case both players are alive, calculate the middle position between both players as the target location
        if (GameManager.instance.getIsAlive(true) && GameManager.instance.getIsAlive(false))
            centerPointBetweenPlayers = (playerA.position + playerB.position) * 0.5f;
        // In case only one player is alive, get the position of the living player as the target location
        else
        {
            if (GameManager.instance.getIsAlive(true))
                centerPointBetweenPlayers = playerA.position;
            else
                centerPointBetweenPlayers = playerB.position;
        }

        // Move the camera towards the target location smoothly (with a Lerp)
        transform.position = Vector2.Lerp(transform.position, centerPointBetweenPlayers + new Vector2(0,cameraHeight), camSpeed);
    }
}
