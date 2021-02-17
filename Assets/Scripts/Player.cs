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

  //  public GameObject differeence;


    private bool isinthelava = false;
    int numberofjumps = 0;






    // Use this for initialization
    void Start() {

        // rb = GetComponent<Rigidbody2D>();


        toJump = false;

    }

    public static bool isDead()
    {

        if (characterController2D.isdead)
        {
            return true;
        }
        else
        {
            return false;
        }
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
             

            }




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
            
            numberofjumps++;
            

        }
        if (Input.GetMouseButton(0) && controller.isJumping)
        {
            if (controller.jumpTimeCounter > 0)
            {
                controller.fuckingJumpForce();
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

   


   
}
