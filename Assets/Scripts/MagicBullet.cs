using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float count = 10;
    public Player player;

    private Timer timer;

    public int counter = 0;
    private void Awake()
    {
        count = 5;
        timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timeLeft = 3;
        timer.timeDefault = 3;
        timer.running = true;
        timer.Reset();
        counter = 0; 
        //Destroy(gameObject, count); //Destroy object after x seconds
    }

    private void Update()
    {
        if (!timer.running)
        {
            if(counter == 0)
            {
                Debug.Log("Player missed a Shot!");
                player.missedShotsCounter++;
                Destroy(gameObject);
                return; 

            }    
            for(int i = 1; i < counter; i++)
            {
                Debug.Log("Bonus!");
                player.giveCoins(5); //Bonus 
            }

            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        { //Kill goon
            Debug.Log("Player killed a goon!");
            //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.giveCoins(collision.gameObject.GetComponent<EnemyAI>().coins);
            collision.gameObject.GetComponent<EnemyAI>().playDeath();
            //Destroy(gameObject);

            counter++;
            return;
        }
        else if (collision.gameObject.tag == "Witch")
        { //Kill witch once she has taken five shots
            Debug.Log("Hit the Witch!");
            int ret = collision.gameObject.GetComponent<Witch>().getShot();
            if (ret > 0)
            {
                //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                player.giveCoins(ret);
               
            }
            Destroy(gameObject);

        }
        else if (collision.gameObject.tag == "Bear")
        {
            Debug.Log("Hit the boss!");
            int ret = 0;

            if (player.playerID == 2)
            {
                ret = collision.gameObject.GetComponentInParent<Bear>().TakeDamageP2(10);
            }
            else
            {
                 ret = collision.gameObject.GetComponentInParent<Bear>().TakeDamageP1(10);
                
            }
            Destroy(gameObject);


        }
        else{ //Shot is missed
            if(timer.timeLeft == 0)
            {
                Debug.Log("Player missed a Shot!");
                //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                player.missedShotsCounter++;
                Destroy(gameObject);
                //timer.Reset();
            }
           
            
        }
       
    }

    

    


}
