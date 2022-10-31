using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{

    public Bear bear;

    public Timer timer; 
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.timeLeft = 3;
        timer.timeDefault = 3;
        timer.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.timeScale == 0 || Globals.gamePaused)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            bear.Attack(other.gameObject);
            timer.Reset();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.timeScale == 0 || Globals.gamePaused)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
           if(timer.timeLeft == 0) //Attack every 3 seconds
            {
                bear.Attack(other.gameObject);
                timer.Reset();
            }
        }
    }
}
