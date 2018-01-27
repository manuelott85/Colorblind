using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieingSystem : MonoBehaviour {

    public bool isPlayerA = true;

	// Use this for initialization
	void Start () {
		
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
