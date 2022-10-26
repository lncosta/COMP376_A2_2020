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


    // Start is called before the first frame update
    void Start()
    {

        playerSize = player2.GetComponent<MeshCollider>().bounds.size.z; 
        screenBoundary = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        xMin = screenBoundary.x + playerSize;
        xMax = screenBoundary.x * (-1) - playerSize; 
    }

    // Update is called once per frame
    void Update()
    {
        
        xMin = screenBoundary.x - playerSize + camera.transform.position.x;
        xMax = screenBoundary.x * (-1) + playerSize + +camera.transform.position.x;
        DistanceCorrection();
    }

    void DistanceCorrection()
    {
        if(player1 != null && player2 != null)
        {
            Vector3 distance = player1.transform.position - player2.transform.position;
            float magnitude = distance.magnitude;
            

            if(Math.Abs(magnitude) > 10)
            {
                


            }

            Vector3 updatedPos = player2.transform.position; 
            updatedPos.x = Mathf.Clamp(player2.transform.position.x, xMax, xMin);


            player2.transform.position = updatedPos; 


        }
    }
}
