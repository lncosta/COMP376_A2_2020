using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGameMenu : MonoBehaviour
{
    public void LoadScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void GameQuit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
