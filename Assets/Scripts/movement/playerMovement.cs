using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Tooltip("The amount of speed that is applied by pushing the stick to either side at its maximum")]
    public float moveForce = 20;
    [Tooltip("How much the player is able to jump")]
    public float jumpForce = 5;
    [Tooltip("Decrease this value if jumping midair is possible; Increase this value if the player is on the ground but still can not jump")]
    public float minVelocity = 0.5f;

    //Inputs, both are set by the calibration system and spread out by the gameManager
    private string forceValueRL; //The name of the axis for left and right movement
    private string forceButtonA; //The name of the key that is used for jumping
    /// <summary>
    /// Tell the movingPlayer script the name of the axis to listen on for moving left and right
    /// </summary>
    /// <param name="name">Name of the axis</param>
    public void setForceValueRL(string name) { forceValueRL = name; }
    /// <summary>
    /// Tell the movingPlayer script the name of the button to jump
    /// </summary>
    /// <param name="name">The name of the jump button</param>
    public void setForceButtonA(string name) { forceButtonA = name; }

    [Tooltip("Drag on X-Axis")]
    [Range(0.001f, 1f)]
    public float XSlowFactor = 1;
    [Tooltip("Drag on Y-Axis")]
    [Range(0.001f, 1f)]
    public float YSlowFactor = 1;

    [Tooltip("The maximum speed (Up/Down)")]
    public float maxYspeed = 20;
    [Tooltip("The maximum speed (Left/Right)")]
    public float maxXspeed = 20;
    
    private SoundSource soundS;
    [Tooltip("Path to the Jump Sound File")]
    public AudioClip jumpSound;
    
    private Rigidbody2D rBodyRef;

    // Use this for initialization
    void Start()
    {
        soundS = GetComponent<SoundSource>();   // Get the reference to the SoundSource
    }

    // Update is called once per frame
    void Update()
    {
        // In case the calibration is not finished, stop here because there are no axes asigned
        if (/*forceValueRL == "" || forceButtonA == "" || */calibration.instance.getIsCalibrationActive())
            return;

        rBodyRef = GetComponent<Rigidbody2D>();   // get a reference to the rigidbody

        //Moving Left and Right
        float forceValueNum = Input.GetAxis(forceValueRL);

        // Move right
        if (
                forceValueNum > 0 &&    // Proceed only if the player wants to move right
                GetComponent<grabFix>().getIsCollidingRight() == false  // this line prevent the player to glue himself on walls
           )
        {
            rBodyRef.AddForce(new Vector2(forceValueNum * moveForce, 0));   // accelerate to the right
        }

        // Move left
        if (
                forceValueNum < 0 &&    // Proceed only if the player wants to move left
                GetComponent<grabFix>().getIsCollidingLeft() == false  // this line prevent the player to glue himself on walls

           )
        {
            rBodyRef.AddForce(new Vector2(forceValueNum * moveForce, 0));   // accelerate to the left
        }

        // Jump
        if (Input.GetButton(forceButtonA))
        {
            if (
                    rBodyRef.velocity.y < minVelocity && rBodyRef.velocity.y > -minVelocity &&  // this line prevents the player to jump midair
                    GetComponent<grabFix>().getIsCollidingUp() == false   // this line prevent the player to glue himself at the ceiling
               )
            {
                rBodyRef.AddForce(new Vector2(0,  jumpForce),ForceMode2D.Impulse);  // Jump

                // Play the sound
                if (soundS != null && jumpSound != null)
                    soundS.playAudio(jumpSound, 0, transform.position, false);
            }
        }


        // Handling Max Speed 
        // if the max speed has been reached, clamp it

        // X-Axis
        if (rBodyRef.velocity.x > maxXspeed)   // Right
        {
            rBodyRef.velocity = new Vector2(maxXspeed, rBodyRef.velocity.y);
        }
        else if (rBodyRef.velocity.x < -maxXspeed) // Left
        {
            rBodyRef.velocity = new Vector2(-maxXspeed, rBodyRef.velocity.y);
        }
        // Y-Axis
        if (rBodyRef.velocity.y > maxYspeed)   // Up
        {
            rBodyRef.velocity = new Vector2(rBodyRef.velocity.x, maxYspeed);
        }
        else if (rBodyRef.velocity.y < -maxYspeed) // Down
        {
            rBodyRef.velocity = new Vector2(rBodyRef.velocity.x, -maxYspeed);
        }
    }

    private void FixedUpdate()
    {
        // Slowdown the player seperated for each axes (x-drag, y-drag)
        rBodyRef = GetComponent<Rigidbody2D>();

        float yDrag = rBodyRef.velocity.y * YSlowFactor;

        // optional increasing the speed when falling down
        //if (rBodyRef.velocity.y < 0)
        //    yDrag = rBodyRef.velocity.y * (2 - YSlowFactor);

        rBodyRef.velocity= new Vector2(rBodyRef.velocity.x * XSlowFactor, yDrag);
    }
}
