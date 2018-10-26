using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateInputAndCollision : MonoBehaviour
{
    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "If stepping into this trigger 2D, this script will disable the plays's input. Optionally, it can disable the collision with the camera boundaries";

    [Tooltip("If set to true: the player will no longer collide with the camera boundaries until being revived")]
    public bool bPreventCameraCollision = false;

    SoundSource soundS;
    [Tooltip("The sound played when the player steps into the trigger")]
    public AudioClip soundSourceFile;

    // Use this for initialization
    void Start()
    {
        soundS = GetComponent<SoundSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Player enters the area and deaktivate the player's input
        playerMovementNew movementScript = collision.transform.parent.GetComponent<playerMovementNew>();
        if (movementScript)
            movementScript.bDeactivateControlls = true;

        // if is player and should deactivate the collision with the camera boundaries: switch physical layer to default
        if (movementScript && bPreventCameraCollision)
            collision.transform.gameObject.layer = 0;

        // Play some specific sound file to let the player notice, he is dead
        if (soundS != null && soundSourceFile != null)
            soundS.playAudio(soundSourceFile, 0, transform.position, false);
    }
}
