using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public static class Globals
{
    //Set Game global variables:
    public static int numPlayers = 1;

    public static int hitContinue = 0;
}

public class MainMenu : MonoBehaviour
{
   private TextMeshProUGUI text;

   public void LoadScene(int x)
    {
        SceneManager.LoadScene(x);
    }
   public void PlayLevel1()
    {
        Debug.Log("Level 1");
        SceneManager.LoadScene(1); 
    }

   public void SetPlayerNum()
    {
        text = GameObject.Find("Number of Players Text").GetComponent<TextMeshProUGUI>(); 
        if(Globals.numPlayers == 4)
        {
            Globals.numPlayers = 1;
        }
        else
        {
            Globals.numPlayers++;
        }

        text.SetText("Number of Players: " + Globals.numPlayers);
    }

    public void GameQuit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
