using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public Timer timer1;
    public GameObject timerSlot;
    public GameObject continueCanvas;

    public int sceneLoadIndex = 1;

    public Player[] players;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        bool oneDead = false;
        foreach(Player player in players)
        {
            if (player.dead)
            {
                oneDead = true;
                Time.timeScale = 0;
                continueCanvas.SetActive(true);
            }
        }

        if (oneDead && !timer1.running)
        {
            timer1.Reset();
        }
        if (timer1.running)
        {
            float sec = Mathf.FloorToInt(timer1.timeLeft % 60);
            float min = Mathf.FloorToInt(timer1.timeLeft / 60);

            timerSlot.GetComponent<TextMeshProUGUI>().SetText(string.Format("{0:00}:{1:00}", min, sec));

            if(timer1.timeLeft < 1)
            {
                BackToMain();
            }

        }
        else
        {
            timerSlot.GetComponent<TextMeshProUGUI>().SetText("00:00");
        }
    }

    public void Continue()
    {
        Globals.hitContinue++;
        if(Globals.hitContinue < 4)
        {
            foreach (Player player in players)
            {
                if (player.dead)
                {
                   player.dead = false;
                    player.lives = 3;
                }
            }
            Time.timeScale = 1;
            continueCanvas.SetActive(false);
            //SceneManager.LoadScene(sceneLoadIndex, LoadSceneMode.Single);
        }
        else
        {
            BackToMain();
        }
       
    }

    public void BackToMain()
    {
        foreach (Player player in players)
        {
            if (player.dead)
            {
                player.dead = false;
                player.lives = 3;
            }
        }
        Time.timeScale = 1;
        continueCanvas.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
