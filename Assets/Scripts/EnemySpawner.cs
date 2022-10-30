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


    public GameObject boss; 
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < numberToSpawn; i++)
        {
            float x = Random.Range(marker1.transform.position.x, marker2.transform.position.x);
            Instantiate(goonPrefab, new Vector3(x, yaxis, zaxis), Quaternion.Euler(0, 45, 0));  //Instantiates goons within position range
        }

        timer = gameObject.AddComponent<Timer>();
        timer.timeLeft = 15;
        timer.timeDefault = 15;

        boss.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        float chance = Random.Range(0, 1);
        if (timer.timeLeft == 0 && enemyWaves > 0)
        { //Spawn waves of enemies every x seconds, with a max number of waves per level

            if (chance < 0.25 && numberWitchSpawned < 2)
            {
                numberWitchSpawned++;
                float x = Random.Range(marker1.transform.position.x, marker2.transform.position.x);
                Instantiate(witchPrefab, new Vector3(x, 1.4f, zaxis), Quaternion.Euler(0, 90, 0));  //Instantiates witch within position range
                witchSpawnSound.Play();

            }

            for (var i = 0; i < numberToSpawn / 2; i++)
            {
                float x = Random.Range(marker1.transform.position.x, marker2.transform.position.x);
                Instantiate(goonPrefab, new Vector3(x, yaxis, zaxis), Quaternion.Euler(0, 45, 0));  //Instantiates goons within position range
            }
            timer.Reset();
            enemyWaves--;
            enemySpawnSound.Play();
        }

        //Spawning Boss after all waves are done:
        if(enemyWaves <= 0)
        {
            enemySpawnSound.Play(); 
            boss.SetActive(true); 
        }

        
        
    }
}
