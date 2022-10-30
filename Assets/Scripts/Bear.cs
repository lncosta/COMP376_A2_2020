using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{

    public Animator animator;
    public int hp = 100;

    public Transform spawnLocation;
    public GameObject MagicSphere;
    public float speed = 10;

    public AudioSource soundEffect;

    public int coinsToGive = 100;

    public int p1hits = 0; 
    public int p2hits = 0;

    public GameObject playerHandler;

    public GameObject bearObject;

    private Timer timer;

    public int attackFrequency = 5; 

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timeLeft = 5;
        timer.timeDefault = attackFrequency;
        timer.running = true;
        timer.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer.running || Input.GetKeyDown(KeyCode.J))
        {
            ShootFire();
            timer.Reset();
        }
    }

    public void Attack(GameObject player)
    {
        Player p = player.GetComponent<Player>();

        p.TakeDamage();

        soundEffect.Play();
        animator.Play("Attack1");
    }

    public void ShootFire()
    {

        for(int i = -2; i < 5; i++)
        {
            var sphere = Instantiate(MagicSphere, spawnLocation.position + new Vector3(0, i, 0), spawnLocation.rotation);

            sphere.GetComponent<Rigidbody>().velocity = -spawnLocation.forward * speed;
        }
        
        soundEffect.Play();
        animator.Play("Attack3");
        
    }

    public int TakeDamageP1(int dmg)
    {
        hp = hp - dmg;
        p1hits++; 
        if(hp <= 0)
        {
            
            
            Die();
            return 1;

        }
        else
        {
            return 0;
        }
    }

    public int TakeDamageP2(int dmg)
    {
        hp = hp - dmg;
        p2hits++; 
        if (hp <= 0)
        {
           
            Die();
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public void Die()
    {
        Debug.Log("The boss has died!"); 
        if(p2hits > p1hits)
        {
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(2, coinsToGive);
        }
        else if (p2hits < p1hits)
        {
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(1, coinsToGive);
        }
        else
        {
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(2, coinsToGive/2);
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(1, coinsToGive/2);
        }

        animator.Play("Death");
        Destroy(gameObject, 2); 
    }
}
