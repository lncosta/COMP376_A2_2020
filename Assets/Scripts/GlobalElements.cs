using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


public class PlayerData
{
    public GameObject prefab;
    public int id;
    public int coins;
    public int lives;

    public int specialModeLootHeld; 
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

    public static int currentLevel = 0;

    public static bool gamePaused = false;

    public static bool wasReset = false;

    public static bool specialModeOverride = false;

    public static string didTheMostDamage = "";

    public static bool oneDead = false; 

    public static void resetAll()
    {
        p1 = null;
        p2 = null;
        wasReset = true;
        specialModeOverride = false;
        currentLevel = 0;
        didTheMostDamage = "";
        oneDead = false;
    }


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


