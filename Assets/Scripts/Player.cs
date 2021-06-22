using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class Player : MonoBehaviour {

    [System.Serializable]
    public class Player_Details
    {
        [System.Serializable]
        public struct Details
        {
            public string Name;
            public string Date;
            public long Score;
        }
        public Details player_details;

    }


    
    [SerializeField]
    private Animator jump;
    [SerializeField]
    private static bool toJump;
    [SerializeField]
    private characterController2D controller;
    private float wait_time = 0;
    
    public static long Score
    {
        get; set;
    }

    public static bool isDead 
    {
        get;set;
    }
   
    

    void Start() {

  

        isDead = false;
        toJump = false;

    }





    // Update is called once per frame
    void Update()
    {
        if (FitDatCamera.gamestarted)
        {
          

            if (FitDatCamera.tutorialdeact)
            {
                //touch screen contorlls
                controls();
                DistanceMade();

            }




        }
    }
    private void DistanceMade()
    {

        wait_time += Time.deltaTime * 2f;
        if (wait_time >= 1)
        {
            Score++;
            wait_time = 0;
        }



    }

    void controls()
    {
        if (Input.GetMouseButtonDown(0) && controller.isLanded)
        {



            audioManager.jumpingAudioSource.Play();

            toJump = true;
            jump.SetBool("isJumping", true);

           

        }
     
        if (Input.GetMouseButton(0) && controller.isJumping)
        {
            
            if (controller.jumpTimeCounter > 0)
            {
                controller.JumpForce();
                controller.jumpTimeCounter -= Time.deltaTime;
                jump.SetBool("isJumping", true);
             
            }
           
        }
        else
        {
            controller.isJumping = false;
        }



        controller.Jump(toJump);
        toJump = false;



        if (controller.isFalling)
        {
            jump.SetBool("isJumping", true);
            jump.Play("jump", 0, 0.5f);
       
        }
    }

  
    public void OnLanding()
    {
        jump.SetBool("isJumping", false);
     //   Debug.Log("KASJJHDKSHDF");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "lava")
        {

            isDead = true;
            Destroy(gameObject);


        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "parent of column")
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }



}
