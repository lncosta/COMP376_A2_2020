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
    

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButton(player.mouse1))
        {
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
           

        if (Input.GetButtonDown(player.mouse0))
        {
            var sphere = Instantiate(MagicSphere, spawnLocation.position, spawnLocation.rotation);
            sphere.GetComponent<Rigidbody>().velocity = player.transform.forward * speed;
            soundEffect.Play();
            player.shoot();
        
        }
        



    }


}

