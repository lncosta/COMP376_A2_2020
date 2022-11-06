using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

        Debug.Log("Player Handler Active");
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
        else
        {
            player2.SetActive(true);
        }

        //Special Mode Handling:
        if (Globals.specialModeOverride)
        {
            Globals.specialMode = true;
            specialModeTimer = gameObject.AddComponent<Timer>();
            specialModeTimer.running = true;
            specialModeTimer.timeLeft = 60*60*1000;
            TriggerSpecialModeOverride();
        }
        else
        {
            Globals.specialMode = false;
            specialModeTimer = gameObject.AddComponent<Timer>();
            specialModeTimer.timeDefault = specialModeTimeDuration;
            specialModeTimer.running = false;
            specialModeTimer.timeLeft = 0;
        }
        
       

        LoadPlayerData(); 
    }

    // Update is called once per frame
    void Update()
    {

        //Special Mode Handling:

        if (Input.GetKeyDown(KeyCode.Tab) && !Globals.specialMode && !Globals.specialModeOverride)
        {
            Globals.specialMode = true;
            specialModeTimer.Reset();
            specialModeSound.Play();
            specialModeLight.SetActive(true);
        }

        if (!specialModeTimer.running && !Globals.specialModeOverride)
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
    public void TriggerSpecialMode()
    {
        Globals.specialMode = true;
        specialModeTimer.Reset();
        specialModeSound.Play();
        specialModeLight.SetActive(true);
    }

    public void TriggerSpecialModeOverride()
    {
        Globals.specialMode = true;
        specialModeTimer.running = true; 
        specialModeSound.Play();
        //specialModeLight.SetActive(true);
    }

    void DistanceCorrection()
    {
            Vector3 updatedPos = player2.transform.position; 
            updatedPos.x = Mathf.Clamp(player2.transform.position.x, xMax, xMin);
            updatedPos.z = player1.transform.position.z;
            player2.transform.position = updatedPos; 

        
    }

    public void SavePlayerData()
    { //Storing player data between levels
        Debug.Log("Player Handler Saving Player Data.");
        if (player1 != null)
        {
            
            Globals.p1.coins = player1.GetComponent<Player>().coins;
            Globals.p1.lives = player1.GetComponent<Player>().lives;
            Globals.p1.id = 1;
            Globals.p1.specialModeLootHeld = player1.GetComponent<Player>().specialModeLootHeld;
            Globals.p1.prefab = player1;

            //Debug.Log("Player 1: coins -" + Globals.p1.coins);
        }

        if (Globals.numPlayers > 1 && player2 != null)
        {


            Globals.p2.coins = player2.GetComponent<Player>().coins;
            Globals.p2.lives = player2.GetComponent<Player>().lives;
            Globals.p2.id = 1;
            Globals.p2.specialModeLootHeld = player2.GetComponent<Player>().specialModeLootHeld;
            Globals.p2.prefab = player2;
        }

        
    }

    public void LoadPlayerData()
    {
        Debug.Log("Player Handler Loading Player Data.");
        Debug.Log("Number of Active Players: " + Globals.numPlayers);
        if (Globals.wasReset)
        {
            //Player initialization:

            if (Globals.numPlayers < 2)
            {

                if (player2 != null)
                {
                    player2.SetActive(false);
                }
            }
            else
            {
                player2.SetActive(true);
            }

            Globals.wasReset = false; 

        }
        if (player1 != null)
        {
            if(Globals.p1 == null)
            {
                Globals.p1 = new PlayerData();
                Globals.p1.coins = 0;
                Globals.p1.lives = 3;
                Globals.p1.id = 1;
                Globals.p1.specialModeLootHeld = 1;
                Globals.p1.prefab = player1;
            }
           
            player1.GetComponent<Player>().coins = Globals.p1.coins;
            player1.GetComponent<Player>().lives = Globals.p1.lives;
            player1.GetComponent<Player>().specialModeLootHeld = Globals.p1.specialModeLootHeld;

            Debug.Log("Player 1: coins -" + player1.GetComponent<Player>().coins);

        }

        if (Globals.numPlayers > 1 && player2 != null)
        {
            if(Globals.p2 == null){
                Globals.p2 = new PlayerData();
                Globals.p2.coins = 0;
                Globals.p2.lives = 3;
                Globals.p2.id = 1;
                Globals.p2.specialModeLootHeld = 1;
                Globals.p2.prefab = player2;
            }
            player2.GetComponent<Player>().coins = Globals.p2.coins;
            player2.GetComponent<Player>().lives = Globals.p2.lives;
            player2.GetComponent<Player>().specialModeLootHeld = Globals.p2.specialModeLootHeld;
        }

        Globals.gamePaused = false;
        Time.timeScale = 1;
    }

    public void AwardPoints(int id, int coins)
    {
        if(id == 2)
        {
            if(player2 != null)
            {
                player2.GetComponent<Player>().coins += coins;
                player2.GetComponent<Player>().specialModeLootHeld++;
            }

        }
        else 
        {
            if (player1 != null)
            {
                player1.GetComponent<Player>().coins += coins;
                player1.GetComponent<Player>().specialModeLootHeld++;
            }

        }
    }


}
