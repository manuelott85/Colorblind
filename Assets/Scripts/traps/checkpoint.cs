using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "Add this component to convert the object into a checkpoint";

    private bool isActive = false;
    private SoundSource soundS;

    [Tooltip("A reference to the sprite, that is used when this checkpoint gets activated")]
    public Sprite activeVersion;
    [Tooltip("Soundclip played, when a character gets revived here")]
    public AudioClip reviveSound;
    [Tooltip("Soundclip played, when this checkpoint gets activated")]
    public AudioClip activeSound;

    // Use this for initialization
    void Start () {
        soundS = GetComponent<SoundSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// If a Players enters the trigger zone, activate the checkpoint if not already done
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // In case a player enters the trigger zone and it hasn't been activated: activate it
        if ((collision.transform.parent.gameObject == GameManager.instance.playerA.gameObject ||
             collision.transform.parent.gameObject == GameManager.instance.playerB.gameObject)
             && !isActive)
        {
            isActive = true;    // Set the checkpoint to active
            GameManager.instance.lastActiveCheckpoint = transform;  // register self as the last activated checkpoint (the one that should be used from now on)
            GetComponent<SpriteRenderer>().sprite = activeVersion;  // change the visual representation to the active sprite

            // play the sound clip if possible
            playReviveSound();
        }
    }

    public void playReviveSound()
    {
        if (soundS != null && activeSound != null)
            soundS.playAudio(activeSound, 0, transform.position, false);
    }

    /// <summary>
    /// While a player is standing inside the trigger zone, revive the other player if he is no longer alive
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Execute when player A is in the zone and alive
        if (collision.transform.parent.gameObject == GameManager.instance.playerA.gameObject && GameManager.instance.getIsAlive(true))
        {
            // Execute when player B is dead
            if(!GameManager.instance.getIsAlive(false))
            {
                GameManager.instance.playerB.transform.position = transform.position; // move the character model of player B to this checkpoint
                GameManager.instance.revivePlayer(false); // Tell the game manager to revive player B

                // play the sound clip if possible
                if (soundS != null && activeSound != null)
                    soundS.playAudio(reviveSound, 0, transform.position, false);
            }
        }

        // Execute when player B is in the zone and alive
        if (collision.transform.parent.gameObject == GameManager.instance.playerB.gameObject && GameManager.instance.getIsAlive(false))
        {
            // Execute when player A is dead
            if (!GameManager.instance.getIsAlive(true))
            {
                GameManager.instance.playerA.transform.position = transform.position;  // move the character model of player A to this checkpoint
                GameManager.instance.revivePlayer(true); // Tell the game manager to revive player A

                // play the sound clip if possible
                if (soundS != null && activeSound != null)
                    soundS.playAudio(reviveSound, 0, transform.position, false);
            }
        }
    }
}
