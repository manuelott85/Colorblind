using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killThePlayer : MonoBehaviour {

    SoundSource soundS;
    public AudioClip soundSourceFile;

    // Use this for initialization
    void Start () {
        soundS = GetComponent<SoundSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerA")
            GameManager.instance.killPlayer(true);
        if (collision.gameObject.name == "PlayerB")
            GameManager.instance.killPlayer(false);
        if (soundS != null && soundSourceFile != null)
            soundS.playAudio(soundSourceFile, 0, transform.position, false);
    }
}
