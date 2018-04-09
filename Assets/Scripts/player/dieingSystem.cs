using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieingSystem : MonoBehaviour {
    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This script checks if the player should be dead. In that case, it process every step to apply 'being dead' to the player.";

    [Tooltip("Executes this script on player one or two?")]
    public bool isPlayerA = true;
    [Tooltip("The sound played when the character dies")]
    public AudioClip respawnSound;

    SoundSource soundS; // Reference to the sound source

    // Use this for initialization
    void Start () {
        // Initialize the sound source (Check for a valid one; if failed: create a new one)
        soundS = GetComponent<SoundSource>();
        if (soundS != null && respawnSound != null)
            soundS.playAudio(respawnSound, 0, transform.position, false);
    }
	
	// Update is called once per frame
	void Update () {

        // ToDo: replace by an event driven system
        // In case the player has just died: execute following code
        if (!GameManager.instance.getIsAlive(isPlayerA) && !GameManager.instance.hasInformed(isPlayerA))
        {
            GameManager.instance.ackInformation(true);

            Debug.Log(transform.name + ": I am Dead... animate me");
            transform.gameObject.SetActive(false);
        }
	}
}
