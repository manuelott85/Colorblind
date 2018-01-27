using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {

    private bool isActive = false;
    public Sprite activeVersion;

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
            if(!isActive)
            {
                isActive = true;
                GameManager.instance.lastActiveCheckpoint = transform;
                GetComponent<SpriteRenderer>().sprite = activeVersion;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerA" && GameManager.instance.getIsAlive(true))
        {
            if(!GameManager.instance.getIsAlive(false))
            {
                GameManager.instance.playerB.transform.position = GameManager.instance.lastActiveCheckpoint.position;
                GameManager.instance.revivePlayer(false);
            }
        }
        if (collision.gameObject.name == "PlayerB" && GameManager.instance.getIsAlive(false))
        {
            if (!GameManager.instance.getIsAlive(true))
            {
                GameManager.instance.playerA.transform.position = GameManager.instance.lastActiveCheckpoint.position;
                GameManager.instance.revivePlayer(true);
            }
        }
    }
}
