using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour {

    public void ExitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }
}
