using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public GameObject player1 = null;
    public GameObject enemyObject = null;

    public Camera camera;

    public float mFollowSpeed = 5.0f;
    public float playerSize = 1.0f;

    public float xMin, xMax;
    public Vector2 screenBoundary;

    public float amp = 1;
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerSize = (enemyObject.GetComponent<MeshCollider>().bounds.size.x);
        screenBoundary = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        
        //xMin = screenBoundary.x + playerSize;
        //xMax = screenBoundary.x * (-1) - playerSize;

        xMin = camera.transform.position.x;
        xMax = camera.transform.position.x;


    }

    void DistanceCorrection()
    {
        Vector3 updatedPos = enemyObject.transform.position;
        updatedPos.x = Mathf.Clamp(enemyObject.transform.position.x, xMax, xMin);
        enemyObject.transform.position = updatedPos;


    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
           

            //xMin = screenBoundary.x - playerSize + camera.transform.position.x;
            //xMax = screenBoundary.x * (-1) + playerSize + +camera.transform.position.x;

            xMin = camera.transform.position.x;
            xMax = camera.transform.position.x; 

            DistanceCorrection();

            float prev_y = enemyObject.transform.position.y;
            float prev_x = enemyObject.transform.position.x;

            Vector3 new_pos = new Vector3(enemyObject.transform.position.x, prev_y - amp * Mathf.Sin(speed * Time.time), enemyObject.transform.position.z);

            enemyObject.transform.position = new_pos;
        }
        
    }
}
