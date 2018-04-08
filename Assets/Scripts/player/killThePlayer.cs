﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killThePlayer : MonoBehaviour {

    SoundSource soundS;
    [Tooltip("The sound played when the player steps into the area")]
    public AudioClip soundSourceFile;

    // Use this for initialization
    void Start () {
        soundS = GetComponent<SoundSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// If a player enters the area, kill him
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Player One or Two enters the area and tell the game manager that this player
        // is dead now. By telling the game manager, the players' dieing system script will be executed
        if (collision.gameObject.name == "PlayerA")
            GameManager.instance.killPlayer(true);
        if (collision.gameObject.name == "PlayerB")
            GameManager.instance.killPlayer(false);

        // Play some specific sound file to let the player notice, he is dead
        if (soundS != null && soundSourceFile != null)
            soundS.playAudio(soundSourceFile, 0, transform.position, false);
    }
}
