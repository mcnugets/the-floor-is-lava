using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour {


   

    public Animator jump;
    public static bool toJump;
    public float suka = 5.0f;
    public characterController2D controller;
    private float time = 0;

    public static bool isDead 
    {
        get;set;
    }
    public static long score
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
                distanceScore();

            }




        }
    }
    public void distanceScore() 
    {
        Debug.Log("SCORE----> " + score);
        Debug.Log("time " + time);
        time+=Time.deltaTime*2f;
        if (time >= 1)
        {
            score++;
            time = 0;
        }


       
    }
 
    void controls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (controller.islanded())
            {
                audioManager.jumpingAudioSource.Play();
            }
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
            else
            {
                controller.isJumping = false;
            }

        }
        if (Input.GetMouseButtonUp(0))
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
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "lava")
        {
            //      isdead = false;

        }
    }



}
