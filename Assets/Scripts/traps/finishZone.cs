using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishZone : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This component loads a new scene when either player one or two enters the trigger zone";

    [Tooltip("Name of the scene to load (need to match with the scene manager)")]
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
            if(winScreenLevel != null)
                SceneManager.LoadScene(winScreenLevel);
        }
    }
}
