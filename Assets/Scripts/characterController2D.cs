using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class characterController2D : MonoBehaviour {


    public Rigidbody2D rigidbody2D;
    public AnimationState checkforstate;
    [SerializeField] public float jumpForce =0;

    public LayerMask Topsurface;
   
    public Transform groundCheck;
    public float radius = 0.5f;
    public bool topColumn;


    public GameObject column;
   
    public int ground = 0;
    int air = 0;
    Text findtext;

    private bool isontheground;

    public bool isFalling = false;
    public GameObject lavaFloor;




   [HideInInspector] public float jumpTimeCounter;
    public float JumpTime;
    public bool isJumping = false;
    private List<Collider2D> listofColliders; 
    public UnityEvent OnLandEvent;

    public bool isLanded
    {
        get { return isontheground; }
        set { isontheground = value; }
    }
    // Use this for initialization
    void Start() {

        
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();


        listofColliders = new List<Collider2D>();
        
        isontheground = false;
        

      
        topColumn = false;
       
    }
	
	// Update is called once per frame
	void Update () {
        // hittopSurface(column, gameObject);
       

        hitsSurface();


        print("IS LANDED STATE==> " + isLanded);




        if (isLanded)
        {
            OnLandEvent.Invoke();

        }
        if (rigidbody2D.velocity.y < -0.1 )
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
       


        if (findtext == null)
            return;
        findtext = GameObject.Find("Number of jumps").GetComponent<Text>();
        findtext.text = ground.ToString();

    }
    public void Jump(bool toJump)
    {
        if (isLanded && toJump)
        {
               isJumping = true;
            jumpTimeCounter = JumpTime;
            JumpForce();




        }

    }
    public void JumpForce()
    {
        rigidbody2D.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void hitsSurface()
    {
        topColumn = Physics2D.OverlapCircle(groundCheck.position, radius, Topsurface);
        
    }



   
    void OnCollisionEnter2D(Collision2D col)
    {


        if (col.collider.tag == "starting point")
        {

          
            isLanded = true;

            ground = air;

        }

      
       
    }
    void OnCollisionExit2D(Collision2D col)
    {
        
        if (col.collider.tag == "starting point")
        {
            isLanded = false;
            // ground = air;
        }
    }

  


}
