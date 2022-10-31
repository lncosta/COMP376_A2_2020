using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public GameObject player1 = null;
    public GameObject player2 = null;

    public Camera camera; 

    public float mFollowSpeed = 5.0f;
    public float playerSize = 1.0f; 

    public float xMin, xMax;
    public Vector2 screenBoundary;

    public int activePlayer = 0;

    public Timer specialModeTimer;

    public int specialModeTimeDuration = 5;

    public AudioSource specialModeSound;
    public AudioSource specialModeSoundEnd;
    public GameObject specialModeLight; 

    // Start is called before the first frame update
    void Start()
    {

        playerSize = player2.GetComponent<MeshCollider>().bounds.size.z; 
        screenBoundary = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        xMin = screenBoundary.x + playerSize;
        xMax = screenBoundary.x * (-1) - playerSize; 


        //Player initialization:

        if(Globals.numPlayers < 2)
        {
            if(player2 != null)
            {
                player2.SetActive(false); 
            }
        }

        //Special Mode Handling:
        Globals.specialMode = false;
        specialModeTimer = gameObject.AddComponent<Timer>();
        specialModeTimer.timeDefault = specialModeTimeDuration;
        specialModeTimer.running = false;
        specialModeTimer.timeLeft = 0; 

        LoadPlayerData(); 
    }

    // Update is called once per frame
    void Update()
    {

        //Special Mode Handling:

        if (Input.GetKeyDown(KeyCode.Tab) && !Globals.specialMode)
        {
            Globals.specialMode = true;
            specialModeTimer.Reset();
            specialModeSound.Play();
            specialModeLight.SetActive(true);
        }

        if (!specialModeTimer.running)
        {
            if (Globals.specialMode)
            {
                Globals.specialMode = false;
                specialModeSound.Stop();
                specialModeLight.SetActive(false);
                specialModeSoundEnd.Play();
            }
           
          
        }



        xMin = screenBoundary.x - playerSize + camera.transform.position.x;
        xMax = screenBoundary.x * (-1) + playerSize + +camera.transform.position.x;
       
        if (Globals.numPlayers == 2 && player1 != null && player2 != null)
        { //If there are two players...
            if (Input.GetKeyDown(KeyCode.L))
            { //Switch player aim control; 

                activePlayer++;

                activePlayer = activePlayer % 2;

                if(activePlayer == 0)
                {
                    player1.GetComponent<Player>().isActive = true;
                    player2.GetComponent<Player>().isActive = false;
                }
                else
                {
                    player2.GetComponent<Player>().isActive = true;
                    player1.GetComponent<Player>().isActive = false;
                }

            }

            DistanceCorrection();
        }

    }

    void DistanceCorrection()
    {
            Vector3 updatedPos = player2.transform.position; 
            updatedPos.x = Mathf.Clamp(player2.transform.position.x, xMax, xMin);
            player2.transform.position = updatedPos; 

        
    }

    public void SavePlayerData()
    { //Storing player data between levels
        if(player1 != null)
        {
            
            Globals.p1.coins = player1.GetComponent<Player>().coins;
            Globals.p1.lives = player1.GetComponent<Player>().lives;
            Globals.p1.id = 1;
            Globals.p1.prefab = player1; 
        }

        if (player2 != null)
        {


            Globals.p2.coins = player2.GetComponent<Player>().coins;
            Globals.p2.lives = player2.GetComponent<Player>().lives;
            Globals.p2.id = 1;
            Globals.p2.prefab = player2;
        }
    }

    public void LoadPlayerData()
    {
        if (player1 != null)
        {
            if(Globals.p1 == null)
            {
                Globals.p1 = new PlayerData();
                Globals.p1.coins = 0;
                Globals.p1.lives = 3;
                Globals.p1.id = 1;
                Globals.p1.prefab = player1;
            }
            player1.GetComponent<Player>().coins = Globals.p1.coins;
            player1.GetComponent<Player>().lives = Globals.p1.lives;
            
        }

        if (Globals.numPlayers > 1 && player2 != null)
        {
            if(Globals.p2 == null){
                Globals.p2 = new PlayerData();
                Globals.p2.coins = 0;
                Globals.p2.lives = 3;
                Globals.p2.id = 1;
                Globals.p2.prefab = player2;
            }
            player2.GetComponent<Player>().coins = Globals.p2.coins;
            player2.GetComponent<Player>().lives = Globals.p2.lives;
        }
    }

    public void AwardPoints(int id, int coins)
    {
        if(id == 2)
        {
            if(player2 != null)
            {
                player2.GetComponent<Player>().coins += coins;
            }

        }
        else 
        {
            if (player1 != null)
            {
                player1.GetComponent<Player>().coins += coins;
            }

        }
    }

}
