using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    public int lives = 5;
    Animator animator;
    public bool dead = false;
    public int coinValue = 50;
    Rigidbody rb;

    public Light innerLight;
    public int lightIntensityDefault = 10;

    public Transform location;

    public Timer deathTimer;
    public int timeForDeath = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Dying", false);
        dead = false;

        deathTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        deathTimer.timeDefault = timeForDeath*3;
        deathTimer.running = false;
        deathTimer.timeLeft = timeForDeath;

        deathTimer.Reset(); 

    }

    // Update is called once per frame
    void Update()
    {
        innerLight.intensity = lightIntensityDefault;

        if (dead || deathTimer.timeLeft == 0)
        {
            playDeath();
        }
    }

    public int getShot()
    {
        lives--;
        innerLight.intensity = 500;
        
        if(lives == 0)
        {
            dead = true;
            
            return 50;
        }
        else
        {
            return -1;
        }
    }

    public void playDeath()
    {
        animator.Play("Die");
        Destroy(gameObject);
    }
}
