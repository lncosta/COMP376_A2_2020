using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    public static int id = 0;

    public int playerID; 

    public Animator animator;
    public static Player instance;
    //AnimatorOverrideController animator;
    public bool isGrounded;
    public bool isSprinting;
    public Vector3 jumpVector;
    public float jumpSpeed = 5;
    public bool isSliding;
    Rigidbody rb;

    public float moveHor;

    public int coins;
    public int lives = 3;

    public Quaternion lookAt;
    public bool isShooting = false;


    public string inputHor = "Horizontal";
    public string inputVert = "Vertical";
    public string mouse0 = "Fire1";
    public string mouse1 = "Fire2";
    public string jump = "Jump";
    public string dash = "Fire3";
    public string pause = "";

    public bool lostLife = false;
    public int missedShotsCounter = 0;
    public bool dead = false;

    public GhostHandler ghost_handler;
    public AudioSource coinSound;
   

    public AudioSource land;

    public bool isActive = false; 
    // Start is called before the first frame update
    void Start()
    {
        id++;

        playerID = id; //Setting 


        rb = gameObject.GetComponent<Rigidbody>();
        isSliding = false;
        coins = 0;
        lives = 3;
        animator = gameObject.GetComponent<Animator>(); //Get animator component

        instance = this;
        animator.SetBool("Idle", true);
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
       // Debug.Log(isGrounded);
       if(Time.timeScale == 0)
        {
            return;
        }

       //Checking for lives lost:

       if(missedShotsCounter > 3)
        {
           
            lives--;
            missedShotsCounter = 0;
            Debug.Log("Player lost a life!");
            ghost_handler.Trigger();
        }

       //Checking for death:

       if(lives < 1)
        {
            dead = true;
            Debug.Log("Player is dead!");
        }


       //Obtaining player input:
        moveHor = Input.GetAxisRaw(inputHor);
        float moveVert = Input.GetAxisRaw(inputVert);
        Vector3 movement = new Vector3(moveHor, 0.0f, moveVert);
        moveVert = 0.0f;
        Vector3 movement_limited = new Vector3(moveHor, 0.0f, moveVert);
      
        //Rotate Player Character
        if (moveHor != 0 || moveVert != 0)
        {
            Quaternion target = Quaternion.LookRotation(movement_limited, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 1000.0f);
            //transform.rotation = target; 
            lookAt = target;


        }
      

        animator.SetFloat("Horizontal", moveHor);
        if(moveHor == 0)
        {
            animator.SetBool("Idle", true);
            isSliding = false;
        }
        else
        {
            animator.SetBool("Idle", false);
            transform.position += movement_limited * Time.deltaTime * 10.0f;

        }


        if (Input.GetButtonDown(jump) && isGrounded)
        {
            
            //Jump Mechanic
            rb.AddForce(jumpVector * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
            isSliding = false;

        }
        else if (Input.GetButtonDown(dash) && isGrounded)
        {
            //Slide or Roll mechanic
            
            if(moveHor > 0)
            {
                animator.Play("SlideRight");
            }
            else if(moveHor < 0)
            {
                animator.Play("SlideRight");
            }
            else
            {
                animator.Play("SlideRight");
            }

            


            Debug.Log("Sliding");
            
        }
        animator.SetBool("Grounded", isGrounded);
        //animator.SetBool("Sliding", isSliding);

        animator.SetBool("Shooting", isShooting);

    }

    public int getScore()
    {
        return coins;
    }

    public int getLives()
    {
        return lives;
    }

    public Quaternion getLookAt()
    {
        return lookAt;
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Floor")
        {
            isGrounded = true;
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.transform.tag == "Floor")
        {
            land.Play();
            Debug.Log("Player hit the floor");
            isGrounded = true;
        }
    }

    public void giveCoins(int coins_added) {
        coins += coins_added;
        coinSound.Play();
    }

    public void shoot()
    {
        animator.Play("Shooting");
    }


}
