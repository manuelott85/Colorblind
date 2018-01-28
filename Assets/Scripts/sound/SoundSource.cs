using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour {
    
    bool soundIsPlaying;
    [Range(0, 1)]
    public float volume = 1;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void playAudio(AudioClip soundSourceFile, float delay, Vector2 location, bool linear)
    {
        playAudio(soundSourceFile, delay, volume, location, linear);
    }

    public void playAudio(AudioClip soundSourceFile, float delay, float volumeLoc, Vector2 position, bool linear)
    {
        PlayClipAt(soundSourceFile, position, volumeLoc, linear);
    }

    AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volumeLoc, bool linear)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position

        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        aSource.spatialize = true;
        aSource.spatialBlend = 0.0f;
        aSource.volume = volumeLoc;
        aSource.rolloffMode = AudioRolloffMode.Linear;
        aSource.Play(); // start the sound

        //SoundMover updateSoundPos = tempGO.AddComponent<SoundMover>();
        //updateSoundPos.targetPosition = transform;

        Destroy(tempGO, clip.length); // destroy object after clip duration

        return aSource; // return the AudioSource reference
    }
}
