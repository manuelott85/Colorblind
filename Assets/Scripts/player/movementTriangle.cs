using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class movementTriangle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The force that is applied as one big push in that moment the player PUSHES the jump button")]
    private float jumpForceImpulse = 50f;
    [SerializeField]
    [Tooltip("This is the amount of force that is applied to the jump while HOLDING the jump button; this value is a multiplicator for the jumpForceImpulse value")]
    private float jumpForceAdditional = 2f;

    [SerializeField]
    [Tooltip("While in this timespan (in sec), the player can hold down the jump button to reach higher levels")]
    private float time4MaxjumpForceInSec = 0.5f;
    [SerializeField]
    [Tooltip("The player will able to jump between the negative of minVelocity, zero and minVelocity")]
    private float minVelocity = 0.1f;

    [SerializeField]
    private float resetCoolDownInSec = 10f;
    private bool resetCoolDownActive = false;
    [SerializeField]
    [Tooltip("Sound played while in the cooldown window")]
    private StudioEventEmitter errorReset;

    [Header("Debug Properties")]
    [SerializeField]
    private bool debug = false;

    Vector2 movementVector = Vector2.zero;
    private Rigidbody2D rBodyRef;
    private float timeStempJumpStart = 0;
    //private bool isJumping = false;
    private bool inJumpProcess = false;
    private float spawnDistance = 0f;
    private bool isColliding = false;
    public void setIsColliding(bool value) { isColliding = value; }
    private Vector3 startPos = Vector3.zero;

    private void Start()
    {
        //GameManager.instance.m_gamePauseEvent.AddListener(setGamePause);
        //GameManager.instance.player = this.gameObject;

        rBodyRef = GetComponent<Rigidbody2D>();

        //spawnDistance = GameManager.instance.autoScrollObj.transform.position.x + transform.position.x;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.P1Btn_A == "")
            return;

        //transform.position = new Vector3(GameManager.instance.autoScrollObj.transform.position.x + spawnDistance, transform.position.y, transform.position.z);

        // Initiate the jump sequence
        if (Input.GetButtonDown(GameManager.instance.P1Btn_A))
        {
            // If the character is not jumping, initiate the jump sequence
            if (rBodyRef.velocity.y < minVelocity && rBodyRef.velocity.y > -minVelocity && !isColliding)  // this line prevents the player to jump midair
            {
                inJumpProcess = true;
                timeStempJumpStart = Time.realtimeSinceStartup;

                movementVector = new Vector2(0, 1f);
                movementVector *= jumpForceImpulse;
                gameObject.GetComponent<Rigidbody2D>().AddForce(movementVector,ForceMode2D.Impulse);
            }
        }

        // Dissalow the player to influence the current jump sequence anymore if he release the jumpbutton once
        if (Input.GetButtonUp(GameManager.instance.P1Btn_A))
        {
            inJumpProcess = false;
        }
    }

    private void FixedUpdate()
    {
        // only proceed if the player was still holding the jumpbutton and is in air
        if (inJumpProcess && timeStempJumpStart + time4MaxjumpForceInSec > Time.realtimeSinceStartup)
        {
            Vector2 movementVectorAdditional = movementVector * jumpForceAdditional;
            gameObject.GetComponent<Rigidbody2D>().AddForce(movementVectorAdditional);
        }
    }
}
