using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{

    public GameObject panel;
    public GameObject points;
    public GameObject sounds;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Globals.gamePaused = true;
            Pause();
            Debug.Log("Paused Game");
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        //points.SetActive(false);
        panel.SetActive(true);
    }

    public void Resume()
    {
        Globals.gamePaused = false;
        Time.timeScale = 1;
        panel.SetActive(false);
        //points.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
