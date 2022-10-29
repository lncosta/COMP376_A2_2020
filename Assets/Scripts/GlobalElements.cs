using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData
{
    public GameObject prefab;
    public int id;
    public int coins;
    public int lives; 
}
public static class Globals
{
    //Set Game global variables:
    public static int numPlayers = 1;

    public static int hitContinue = 0;

    public static GameObject player1;
    public static GameObject player2;

    public static bool specialMode = false;

    public static PlayerData p1 = null;
    public static PlayerData p2 = null;
}


public class GlobalElements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


