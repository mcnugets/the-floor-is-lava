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
    
    public static int numberoflanding;
    public bool isFalling = false;
    public GameObject lavaFloor;



   [HideInInspector] public float jumpTimeCounter;
    public float JumpTime;
    public bool isJumping = false;
    public static bool isdead;
    private bool alreadyDoneDat;
    private List<Collider2D> listofColliders; 

    public UnityEvent OnLandEvent;
    // Use this for initialization
    void Start() {

        
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();


        listofColliders = new List<Collider2D>();
        numberoflanding = 0;
        isontheground = false;
        alreadyDoneDat = false;

      
        topColumn = false;
        isdead = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // hittopSurface(column, gameObject);
       

        hitsSurface();

        if (isdead)
        {
            Debug.Log("NIGGA, HE DEAD");
        }
        Debug.Log("NUMBER OF LANDING------->" + numberoflanding);


      

         if (islanded() == true)
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
        if (islanded() && toJump)
        {
               isJumping = true;
            jumpTimeCounter = JumpTime;
            fuckingJumpForce();




        }

    }
    public void fuckingJumpForce()
    {
        rigidbody2D.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void hitsSurface()
    {
        topColumn = Physics2D.OverlapCircle(groundCheck.position, radius, Topsurface);
        Debug.Log("CHECK " + topColumn);
    }


   /* private enum hitSurface { None, Top };

    private hitSurface hittopSurface(GameObject Object, GameObject objecthit)
    {
        hitSurface hitDirection = hitSurface.None;
        Vector2 direction = Object.transform.position - objecthit.transform.position;

        Ray2D myray2Dy = new Ray2D(objecthit.transform.position, Vector2.up);
        RaycastHit2D myRayHit = Physics2D.Raycast(myray2Dy.origin, myray2Dy.direction);
        if (myRayHit.collider != null)
        {
            topColumn = true;
        }
        else
        {
            topColumn = false;
        }
        /*  RaycastHit myRayHit;
         Vector3 direction = Object.transform.position - objecthit.transform.position;
         
         if (Physics.Raycast(myray, out myRayHit))
         {

             if (myRayHit.collider != null)
             {

                 Vector3 MyNormal = myRayHit.normal;
                 MyNormal = myRayHit.transform.TransformDirection(MyNormal);

                 if (MyNormal == myRayHit.transform.up) { hitDirection = hitSurface.Top; topColumn = true; Debug.Log("TOP"); }


             }
         }
           
        return hitDirection;
      
    }*/

  

    public static int numbahoflandings()
    {
        return numberoflanding++;
    }

   
    void OnTriggerEnter2D(Collider2D col)
    {
          if (col.tag == "lava")
        {
            isdead = true;
            Destroy(gameObject);
            // Physics2D.IgnoreCollision(lavaFloor.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "lava")
        {
            isdead = false;
            
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {


        if (col.collider.tag == "parent of column" && topColumn)
        {

            if (!listofColliders.Contains(col.collider))
            {
                numberoflanding++;
                audioManager.scoreAudioSource.Play();
                listofColliders.Add(col.collider);
            }

            Debug.Log("COLLDINNG");
            isLandedsetter(true);

            ground = air;

        }

        else if (col.collider.tag == "starting point")
        {
            isLandedsetter(true);
            // ground = air;
        }
       
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "parent of column")
        {
            isLandedsetter(false);
            air += 1;
        }
        
        else if (col.collider.tag == "starting point")
        {
            isLandedsetter(false);
            // ground = air;
        }
    }

    public void isLandedsetter(bool isontheground)
    {
        this.isontheground = isontheground;
    }

    public bool islanded()
    {
        return isontheground;
    }



}
