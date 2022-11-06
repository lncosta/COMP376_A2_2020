using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public GameObject player1; 

    public GameObject bearObject;
    public Rigidbody bearObjectRigidbody;

    private Timer timer;

    public int attackFrequency = 5;
    public int enemySpeed = 3;
    public int enemyTypeModifier = 1;

    public GameObject explosionParticles;

    public bool dead = false;

    public Timer deathTimer;

    public string enemyTypeDescription = "Bear";

    public int timeForDeath = 6;

    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timeLeft = 5;
        timer.timeDefault = attackFrequency;
        timer.running = true;
        timer.Reset();

        dead = false;
        deathTimer = gameObject.AddComponent(typeof(Timer)) as Timer; 
        deathTimer.timeDefault = timeForDeath; 
        deathTimer.running = false;
        deathTimer.timeLeft = 0;

        if(deathSound == null)
        {
            deathSound = GameObject.FindGameObjectWithTag("DeathSoundPlayer").GetComponent<AudioSource>();
        }

        p1hits = 0;
        p2hits = 0;

        //Difficulty adjustment for multiplayer mode:
        if (Globals.numPlayers > 1)
        {
            hp *= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer.running || Input.GetKeyDown(KeyCode.J))
        {
            ShootFire();
            //MoveTowardsPlayer();
            timer.timeDefault = (int) (attackFrequency * Random.Range(0.5f, 1.0f)); //Randomize attack frequency
            timer.Reset();
        }
    }

    public void MoveTowardsPlayer()
    {
        Vector3 moveDir = new Vector3((bearObject.transform.position.x - player1.transform.position.x), 0, 0);
        transform.Translate(-moveDir.normalized * enemySpeed);
        Debug.Log(-moveDir.normalized * enemySpeed);

    }

    public void Attack(GameObject player)
    {
        if (dead || Time.timeScale == 0)
        {
            return; 
        }
        Player p = player.GetComponent<Player>();

        if(enemyTypeDescription == "Dragon")
        {
            p.TakeDamage(0.5f);
        }
        else
        {
            p.TakeDamage();
        }

        soundEffect.Play();
        animator.Play("Attack1");
    }

    public void ShootFire()
    {
        if (dead || enemyTypeDescription == "Spider" || Time.timeScale == 0)
        {
            return;
        }
        for(int i = -2 * enemyTypeModifier; i < 5 * enemyTypeModifier; i++)
        {
            GameObject sphere;
            if (enemyTypeDescription == "Dragon")
            {
                sphere = Instantiate(MagicSphere, spawnLocation.position + new Vector3(3*(i + 2), (i *0.1f), 0), spawnLocation.rotation);
                sphere.GetComponent<Fireball>().reduceDamage = true; 
            }
            else
            {
                sphere = Instantiate(MagicSphere, spawnLocation.position + new Vector3(0, i, 0), spawnLocation.rotation);
            }
            

            sphere.GetComponent<Rigidbody>().velocity = -spawnLocation.forward * speed;
        }
        
        soundEffect.Play();
        animator.Play("Attack3");
        
    }

    public int TakeDamageP1(int dmg)
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) {
            return 0; //Negate damage during attack mode to make the fight better paced
        
        }
        
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            return 0; //Negate damage during attack mode to make the fight better paced

        }

        hp = hp - dmg;
        if(Globals.numPlayers > 1)
        {
            p2hits++;
        }
        else
        {
            p1hits++;
        }
        
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
        if (dead)
        {
            return; 
        }
        Debug.Log("The boss has died!");
        dead = true; 
        if(p2hits > p1hits)
        {
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(2, coinsToGive);
            Debug.Log("Player 2 did the most damage and gained a special mode token!");
            Globals.didTheMostDamage = "Player 2 did the most damage and gained a special mode token!";
        }
        else if (p2hits < p1hits)
        {
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(1, coinsToGive);
            Debug.Log("Player 1 did the most damage and gained a special mode token!");
            Globals.didTheMostDamage = "Player 1 did the most damage and gained a special mode token!";
        }
        else
        {
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(2, coinsToGive/2);
            playerHandler.GetComponent<PlayerHandler>().AwardPoints(1, coinsToGive/2);
            Debug.Log("Both players did equal damage and gained a special mode token!");
            Globals.didTheMostDamage = "Both players did equal damage and gained a special mode token!";
        }

        deathSound.Play();
        animator.Play("Death");
        GameObject particles = Instantiate(explosionParticles, bearObject.transform);

        deathTimer.Reset();
        //Destroy(gameObject, 5);
        //Destroy(particles, 6);
    }
}
