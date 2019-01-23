using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour {

    public void ExitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }

    public void StartGamne()
    {
        Debug.Log("LOAD LEVEL1");
        SceneManager.LoadScene(1);
    }
}
