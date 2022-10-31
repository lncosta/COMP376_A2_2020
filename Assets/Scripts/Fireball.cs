using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float count = 30;
    public GameObject enemy;
    private void Awake()
    {
        Destroy(gameObject, count); //Destroy object after x seconds
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        { //Player takes damage
            Debug.Log("Player took damage!");
            collision.gameObject.GetComponent<Player>().TakeDamage();
            Destroy(gameObject);
        }
        

    }
}
