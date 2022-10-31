using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public Timer timer1;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Globals.numPlayers == 1)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins: " + player1.GetComponent<Player>().getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives: " + player1.GetComponent<Player>().getLives());

            string toAdd = "P1 : ";
            for(int i = 0; i < player1.GetComponentInChildren<ShootMagic>().shotsLeft; i++)
            {
                toAdd = toAdd + " o ";
            }

            GameObject.Find("Shots P1").GetComponent<TextMeshProUGUI>().SetText(toAdd);
        }
        else if (Globals.numPlayers == 2)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins (P1): " + player1.GetComponent<Player>().getScore() + "\n" + "Coins (P2): " + player2.GetComponent<Player>().getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives (P1): " + player1.GetComponent<Player>().getLives() + "\n" + "Lives (P2): " + player2.GetComponent<Player>().getLives());
            string toAdd = "P1 : ";
            for (int i = 0; i < player1.GetComponentInChildren<ShootMagic>().shotsLeft; i++)
            {
                toAdd = toAdd + " o ";
            }

            GameObject.Find("Shots P1").GetComponent<TextMeshProUGUI>().SetText(toAdd);

            string toAdd2 = "P2 : ";
            for (int i = 0; i < player2.GetComponentInChildren<ShootMagic>().shotsLeft; i++)
            {
                toAdd2 = toAdd2 + " o ";
            }

            GameObject.Find("Shots P2").GetComponent<TextMeshProUGUI>().SetText(toAdd2);
        }

        /*if (player3 != null)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins (P1): " + player1.getScore() + "\n" + "Coins (P2): " + player2.getScore() + "\n" + "Coins (P3): " + player3.getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives (P1): " + player1.getLives() + "\n" + "Lives (P2): " + player2.getLives() + "\n" + "Lives (P3): " + player3.getLives());

        }
        if (player4 != null)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins (P1): " + player1.getScore() + "\n" + "Coins (P2): " + player2.getScore() + "\n" + "Coins (P3): " + player3.getScore() + "\n" + "Coins (P4): " + player4.getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives (P1): " + player1.getLives() + "\n" + "Lives (P2): " + player2.getLives() + "\n" + "Lives (P3): " + player3.getLives() + "\n" + "Lives (P4): " + player4.getLives());
        }*/

        if (timer1.running)
        {
            float sec = Mathf.FloorToInt(timer1.timeLeft%60);
            float min = Mathf.FloorToInt(timer1.timeLeft/60);

            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().SetText(string.Format("{0:00}:{1:00}", min, sec));

        }
        else
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().SetText("00:00");
        }
        

    }

    
}
