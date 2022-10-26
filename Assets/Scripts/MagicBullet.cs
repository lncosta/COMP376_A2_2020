using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float count = 10;
    public Player player;
    private void Awake()
    {
        Destroy(gameObject, count); //Destroy object after x seconds
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        { //Kill goon
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.giveCoins(collision.gameObject.GetComponent<EnemyAI>().coins);
            collision.gameObject.GetComponent<EnemyAI>().playDeath();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Witch")
        { //Kill witch once she has taken five shots
            Debug.Log("Hit the Witch!");
            int ret = collision.gameObject.GetComponent<Witch>().getShot();
            if (ret > 0)
            {
                Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                player.giveCoins(ret);
               
            }
            Destroy(gameObject);

        }
        else{ //Shot is missed
            Debug.Log("Player missed a Shot!");
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.missedShotsCounter++;
            Destroy(gameObject);
        }
       
    }
}
