using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;

    public Timer timer1;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player1 != null)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins: " + player1.getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives: " + player1.getLives());
        }
        if (player2 != null)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins (P1): " + player1.getScore() + "\n" + "Coins (P2): " + player2.getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives (P1): " + player1.getLives() + "\n" + "Lives (P2): " + player2.getLives());
        }
        if (player3 != null)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins (P1): " + player1.getScore() + "\n" + "Coins (P2): " + player2.getScore() + "\n" + "Coins (P3): " + player3.getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives (P1): " + player1.getLives() + "\n" + "Lives (P2): " + player2.getLives() + "\n" + "Lives (P3): " + player3.getLives());

        }
        if (player4 != null)
        {
            GameObject.Find("Coins").GetComponent<TextMeshProUGUI>().SetText("Coins (P1): " + player1.getScore() + "\n" + "Coins (P2): " + player2.getScore() + "\n" + "Coins (P3): " + player3.getScore() + "\n" + "Coins (P4): " + player4.getScore());
            GameObject.Find("Lives").GetComponent<TextMeshProUGUI>().SetText("Lives (P1): " + player1.getLives() + "\n" + "Lives (P2): " + player2.getLives() + "\n" + "Lives (P3): " + player3.getLives() + "\n" + "Lives (P4): " + player4.getLives());
        }

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
