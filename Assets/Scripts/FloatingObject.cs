using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{

    public float amp = 1;
    public float speed = 2; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            float prev_y = transform.position.y;

            Vector3 new_pos = new Vector3(transform.position.x, prev_y + amp * Mathf.Sin(speed * Time.time), transform.position.z);

            transform.position = new_pos;
        }
        
    }
}
