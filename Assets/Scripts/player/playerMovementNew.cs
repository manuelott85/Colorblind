using UnityEngine;

public class playerMovementNew : MonoBehaviour {

    [SerializeField] [Tooltip("The amount of speed that is applied by pushing the stick to either side at its maximum")]
    private float moveForce = 40f;
    [SerializeField] [Tooltip("Amount of force added when the player jumps")]
    private float m_JumpForce = 400f;
    [SerializeField] [Range(0, .3f)] [Tooltip("How much to smooth out the movement")]
    private float m_MovementSmoothing = .05f;
    [SerializeField] [Tooltip("Whether or not a player can steer while jumping")]
    private bool m_AirControl = false;
    [SerializeField] [Tooltip("A mask determining what is ground to the character")]
    private LayerMask m_WhatIsGround;
    [SerializeField] [Tooltip("A position marking where to check if the player is grounded")]
    private Transform m_GroundCheck;

    //Inputs, both are set by the calibration system and spread out by the gameManager
    private string forceValueRL; //The name of the axis for left and right movement
    private string forceButtonA; //The name of the key that is used for jumping
    public void setForceValueRL(string name) { forceValueRL = name; } // Tell the movingPlayer script the name of the axis to listen on for moving left and right
    public void setForceButtonA(string name) { forceButtonA = name; } // Tell the movingPlayer script the name of the button to jump

    private float horizontalMove = 0f;
    private bool jump = false;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        // In case the calibration is not finished, stop here because there are no axes asigned
        if (calibration.instance.getIsCalibrationActive())
            return;

        // Get the players' input data
        horizontalMove = Input.GetAxisRaw(forceValueRL) * moveForce;
        if(Input.GetButtonDown(forceButtonA))
        {
            jump = true;
        }
	}

    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }

        // Execute Movement
        move(horizontalMove * Time.fixedDeltaTime, jump);   // Move the character
        jump = false;   // reset jump
    }

    public void move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
                Flip(); // ... flip the player.
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
                Flip(); // ... flip the player.
        }

        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
