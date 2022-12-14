using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int numberToSpawn = 10;
    public GameObject goonPrefab;
    public GameObject witchPrefab;

    public GameObject marker1;
    public GameObject marker2;

    public float yaxis = 3;
    public float zaxis = -3;

    private float numberWitchSpawned = 0;

    public int enemyWaves = 5;

    public Timer timer;

    public AudioSource enemySpawnSound;
    public AudioSource witchSpawnSound;

    public int spawnSpecialModifier = 1;

    public bool bossHasBeenSummoned = false;

    public GameObject levelCompletePanel; 


    public GameObject boss;

    public AudioSource winSound;

    public int timeBetweenWaves = 15;

    

    
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < numberToSpawn; i++)
        {
            float x = Random.Range(marker1.transform.position.x, marker2.transform.position.x);
            Instantiate(goonPrefab, new Vector3(x, yaxis, zaxis), Quaternion.Euler(0, 45, 0));  //Instantiates goons within position range
        }

        timer = gameObject.AddComponent<Timer>();
        timer.timeLeft = timeBetweenWaves;
        timer.timeDefault = timeBetweenWaves;

        spawnSpecialModifier = 1; 
        boss.SetActive(false);
        bossHasBeenSummoned = false;

       

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 || Globals.gamePaused)
        {
            return;
        }

        if (Globals.specialMode)
        {
            spawnSpecialModifier = 2;
        }
        else
        {
            spawnSpecialModifier = 1;
        }

        //Difficulty adjustment for multiplayer mode:

        if(Globals.numPlayers > 1)
        {
            spawnSpecialModifier += 1;
        }

        float chance = Random.Range(0.0f, 1.0f); //Randomize the chance of the witch spawning
        if (timer.timeLeft == 0 && enemyWaves > 0)
        { //Spawn waves of enemies every x seconds, with a max number of waves per level

            if (chance <= 0.5 && numberWitchSpawned < 4)
            {
                //For every wave, there is a 50% chance of the witch spawning
                numberWitchSpawned++;
                float x = Random.Range(marker1.transform.position.x, marker2.transform.position.x);
                Instantiate(witchPrefab, new Vector3(x, 1.4f, zaxis), Quaternion.Euler(0, 90, 0));  //Instantiates witch within position range
                witchSpawnSound.Play();

            }

            for (var i = 0; i < (numberToSpawn * spawnSpecialModifier) / 2; i++)
            {
                float x = Random.Range(marker1.transform.position.x, marker2.transform.position.x);
                Instantiate(goonPrefab, new Vector3(x, yaxis, zaxis), Quaternion.Euler(0, 45, 0));  //Instantiates goons within position range
            }
            //timer.timeDefault = timeBetweenWaves * Random.Range(0.5f, 1);
            timer.Reset();
            enemyWaves--;
            enemySpawnSound.Play();
        }

        //Spawning Boss after all waves are done:
        if(enemyWaves <= 0 && !bossHasBeenSummoned)
        {
            enemySpawnSound.Play(); 
            boss.SetActive(true);
            bossHasBeenSummoned = true; 
        }
        else if (bossHasBeenSummoned)
        {
            if (boss.GetComponentInChildren<Bear>().dead && !boss.GetComponentInChildren<Bear>().deathTimer.running)
            {
                //Boss has been defeated - level complete trigger
                winSound.Play(); 
                Globals.gamePaused = true;
                Time.timeScale = 0;
                levelCompletePanel.SetActive(true);
            }
        }

        
        
    }
}
