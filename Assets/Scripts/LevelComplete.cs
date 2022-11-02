using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    public GameObject playerHandler;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextScene()
    {
        int x = SceneManager.GetActiveScene().buildIndex;
        playerHandler.GetComponent<PlayerHandler>().SavePlayerData();
        Globals.currentLevel = x + 1;
        Globals.gamePaused = false;
        Time.timeScale = 1;
        Debug.Log("New Scene Loaded");
        SceneManager.LoadScene(x + 1);
    }
    public void GameQuit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
