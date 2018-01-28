using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishZone : MonoBehaviour {

    public string winScreenLevel = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerA" || collision.gameObject.name == "PlayerB")
        {
            Application.LoadLevel(winScreenLevel);
        }
    }
}
