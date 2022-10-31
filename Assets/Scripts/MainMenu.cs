using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;


public class MainMenu : MonoBehaviour
{
   private TextMeshProUGUI text;
    public bool howtoplay = false;
    public GameObject instructions; 

    public AudioSource toPlay;

    public GameObject p1prefab;
    public GameObject p2prefab;

   public void LoadScene(int x)
    {
        toPlay.Play();
        Globals.currentLevel = x;
        SceneManager.LoadScene(x);
    }
   public void PlayLevel1()
    {
        generatePlayerData();
        toPlay.Play();
        Globals.currentLevel = 1;
        Debug.Log("Level 1");
        SceneManager.LoadScene(1);
    }

    public void generatePlayerData()
    {
        Globals.p1 = new PlayerData(); 
        Globals.p1.coins = 0;
        Globals.p1.lives = 3;
        Globals.p1.id = 1;
        Globals.p1.prefab = p1prefab;

        if (Globals.numPlayers > 1)
        {

            Globals.p2 = new PlayerData();
            Globals.p2.coins = 0;
            Globals.p2.lives = 3;
            Globals.p2.id = 1;
            Globals.p2.prefab = p2prefab;
        }
    }

   public void SetPlayerNum()
    {
        toPlay.Play();
        text = GameObject.Find("Number of Players Text").GetComponent<TextMeshProUGUI>(); 
        if(Globals.numPlayers >= 2)
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

    public void HowToPlay()
    {
        toPlay.Play();
        howtoplay = true;
        instructions.SetActive(howtoplay);
    }

    public void CloseHowToPlay()
    {
        toPlay.Play();
        if (howtoplay)
        {
            howtoplay = false;
            instructions.SetActive(howtoplay);
        }
    }
}
