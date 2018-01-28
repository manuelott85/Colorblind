using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieingSystem : MonoBehaviour {

    public bool isPlayerA = true;
    SoundSource soundS;
    public AudioClip respawnSound;
    float framecound = 0;
    public float deathTimeAnimation = 4;

    // Use this for initialization
    void Start () {
        soundS = GetComponent<SoundSource>();
        if (soundS != null && respawnSound != null)
            soundS.playAudio(respawnSound, 0, transform.position, false);
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.getIsAlive(isPlayerA))
            framecound = Time.realtimeSinceStartup;

        if (!GameManager.instance.getIsAlive(isPlayerA))// && !GameManager.instance.hasInformed(isPlayerA))
        {
            GameManager.instance.ackInformation(true);
            
            if(framecound + deathTimeAnimation < Time.realtimeSinceStartup)
                transform.gameObject.SetActive(false);
        }
	}
}
