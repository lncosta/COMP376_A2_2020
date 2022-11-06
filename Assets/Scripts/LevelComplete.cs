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

        //bool valid = SceneManager.GetSceneByBuildIndex(x + 1).IsValid();
        bool valid = ((x+1) < SceneManager.sceneCountInBuildSettings);
        if (!valid)
        { //Loop back to the menu at the end; 
            Globals.resetAll();
            Globals.currentLevel = 0;
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(Globals.currentLevel);
        }
        
    }
    public void GameQuit()
    {
        //Loop back to the menu at the end; 
        Globals.resetAll();
        Globals.currentLevel = 0;
        SceneManager.LoadScene(0);
    }
}
