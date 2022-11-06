using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float count = 30;
    public GameObject enemy;
    public bool reduceDamage = false; 
    private void Awake()
    {
        Destroy(gameObject, count); //Destroy object after x seconds
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        { //Player takes damage
            Debug.Log("Player took damage!");
            if (reduceDamage)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(0.05f); //Correction for when multiple projectiles are fired in quick succession
            }
            else
            {
                collision.gameObject.GetComponent<Player>().TakeDamage();
            }
           
            Destroy(gameObject);
        }
        

    }
}
