using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public float level = 1;
    public float speed = 10.0f;

    private float moveHor = 0.0f;
    private bool idle = true;
    private bool walking = false;

    private float prevDirection = 0.0f;

    Animator animator;

    public GameObject zombie;

    public int coins = 10;

    Rigidbody rb;

    public float timeLeft = 30;
    public bool running = false;

    public int specialModeMod = 1; 



    // Start is called before the first frame update
    void Start()
    {
        speed += level * 2; 
        rb = GetComponent<Rigidbody>();
        animator = zombie.GetComponent<Animator>();
        animator.SetBool("Grounded", true);
        animator.SetBool("Idle", true);
        animator.SetBool("Dying", false);

        running = true;

        specialModeMod = 1; //Speed modifier during special mode

    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.specialMode)
        {
            specialModeMod = 2;
        }
        else
        {
            specialModeMod = 1;
        }
        if (running)
        {

            if (timeLeft > 0)
            {
               
                timeLeft -= Time.deltaTime;

            }
            else
            {
                timeLeft = 0;
                running = false;
                Destroy(gameObject); //Goon no longer available and must be destroyed
            }
        }
       

        if (idle)
        {
            StartCoroutine(Walk());
        }

        Vector3 movement_limited = new Vector3(moveHor, 0.0f, 0.0f);

        if (walking && moveHor != 0.0f)
        {
           
            if (moveHor < 0)
            {
                rb.AddForce(movement_limited * speed * specialModeMod);
            }
            else
            {
                rb.AddForce(movement_limited * speed * specialModeMod);
            }

           
            
        }
        else
        {
            if(moveHor != 0)
            {
                Quaternion target = Quaternion.LookRotation(movement_limited, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 1000.0f);
            }
           
            transform.position += movement_limited * Time.deltaTime * speed * specialModeMod;
        }
    }

    IEnumerator Walk()
    {
        int walkWait = Random.Range(1, 2);
        int walkTime = Random.Range(1, 10);
        int walkDirection = Random.Range(-10, 10);
        int rotationTime = Random.Range(1, 5);

        idle = false;

        yield return new WaitForSeconds(walkWait);

        walking = true;
        animator.SetBool("Idle", false);
        yield return new WaitForSeconds(walkTime);

        walking = false;

        if (walkDirection <0)
        {
            moveHor = -1;
            animator.SetFloat("Horizontal", moveHor);
            prevDirection = moveHor;
            yield return new WaitForSeconds(rotationTime);
            moveHor = 0;
            animator.SetFloat("Horizontal", moveHor);
        }
        else
        {
            moveHor = 1;
            animator.SetFloat("Horizontal", moveHor);
            prevDirection = moveHor;
            yield return new WaitForSeconds(rotationTime);
            moveHor = 0;
            animator.SetFloat("Horizontal", moveHor);
        }

        idle = true;
        animator.SetBool("Idle", true);



    }

    public void playDeath()
    {
        animator.Play("Die");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {

            Debug.Log("Player was attacked!");
            Player p = other.gameObject.GetComponent<Player>();

            p.TakeDamage();
            animator.SetTrigger("Attack"); 
           
        }
    }
}
