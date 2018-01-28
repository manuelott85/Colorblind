using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieingSystem : MonoBehaviour {

    public bool isPlayerA = true;
    SoundSource soundS;
    public AudioClip respawnSound;

    // Use this for initialization
    void Start () {
        soundS = GetComponent<SoundSource>();
        if (soundS != null && respawnSound != null)
            soundS.playAudio(respawnSound, 0, transform.position, false);
    }
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.getIsAlive(isPlayerA) && !GameManager.instance.hasInformed(isPlayerA))
        {
            GameManager.instance.ackInformation(true);

            Debug.Log(transform.name + ": I am Dead... animate me");
            transform.gameObject.SetActive(false);
        }
	}
}
