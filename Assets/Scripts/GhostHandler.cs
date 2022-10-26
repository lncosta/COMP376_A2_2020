using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHandler : MonoBehaviour
{
    public GameObject ghost;
    public float timeLeft = 3;
    public bool running = false;

    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
           
            if (timeLeft > 0)
            {
                ghost.SetActive(true);
                timeLeft -= Time.deltaTime;
                
            }
            else
            {
                timeLeft = 0;
                running = false;
                ghost.SetActive(false);
            }
        }
        else
        {
            ghost.SetActive(false);
            running = false;
        }

    }

    public void Trigger()
    {
        ghost.SetActive(true);
        running = true;
        timeLeft = 3;
        sound.Play();
    }
}
