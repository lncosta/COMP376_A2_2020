using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootMagic : MonoBehaviour
{

    private Camera m_Camera;
    private Vector3 mousePosition;
    public Player player;
    public float force_modifier = 50;

    public Transform spawnLocation;
    public GameObject MagicSphere;
    public float speed = 10;

    public AudioSource soundEffect;

    private Timer timer;

    public int shotsLeft = 10; 
    

    // Start is called before the first frame update
    void Start()
    {
        shotsLeft = 10;
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        timer = gameObject.AddComponent<Timer>();
        timer.running = true; 
        timer.timeDefault = 2;
        timer.timeLeft = 2;
        timer.Reset();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 || Globals.gamePaused)
        {
            return;
        }

        if (Input.GetButton(player.mouse1) && player.isActive)
        { //Aim for active player
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 look_position = hit.point - player.transform.position;
                look_position.z = 0;
                Quaternion targetRotation = Quaternion.LookRotation(look_position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);

            }

           
            
        }

        if (Globals.specialMode)
        {
            if (Input.GetButtonDown(player.mouse0)) //Continuous shooting
            { //Shoot magic ball
                var sphere = Instantiate(MagicSphere, spawnLocation.position, spawnLocation.rotation);
                sphere.GetComponent<MagicBullet>().player = player;
                sphere.GetComponent<Rigidbody>().velocity = transform.forward * speed;
                soundEffect.Play();
                player.shoot();

            }
        }
        else
        {
            if (Input.GetButtonDown(player.mouse0) && shotsLeft > 0)
            { //Shoot magic ball
                var sphere = Instantiate(MagicSphere, spawnLocation.position, spawnLocation.rotation);
                sphere.GetComponent<MagicBullet>().player = player;
                sphere.GetComponent<Rigidbody>().velocity = transform.forward * speed;
                soundEffect.Play();
                player.shoot();
                shotsLeft--;
                //Debug.Log(shotsLeft);

            }
        }

        if(timer.timeLeft == 0)
        {
           // Debug.Log(shotsLeft);
            shotsLeft++;
            if(shotsLeft >= 11)
            {
                shotsLeft = 10;
            }
            else if(shotsLeft < 0)
            {
                shotsLeft = 0;
            }
           
            timer.Reset(); 
        }
       
        



    }


}

