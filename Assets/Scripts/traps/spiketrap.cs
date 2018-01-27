using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiketrap : MonoBehaviour {

    public bool greenVersion = true;

    public Sprite spriteRed;

    public Material materialGreen, materialRed;

	// Use this for initialization
	void Start () {
        if(greenVersion)
        {
            GetComponent<SpriteRenderer>().material = materialGreen;
        }
        if (!greenVersion)
        {
            GetComponent<SpriteRenderer>().sprite = spriteRed;
            GetComponent<SpriteRenderer>().material = materialRed;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
