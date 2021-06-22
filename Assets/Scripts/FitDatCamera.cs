using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FitDatCamera : MonoBehaviour {

    private Color startingcolor = Color.white;
    private Color finalcolor = Color.white;
    private Color changetransparency;
    public Screen checkfortrotation;
    public List <GameObject> menuButtons;
    public Animator anim;
    public Text instruction;
    public GameObject activateGameManager;
    public float initialcamerapos;
    private float zoom;
    private float posx;     
    public float cameraSize;
    private float posy;
    public static bool wasScreenTapped;
    public static bool gamestarted;
    public static bool tutorialdeact;

    private GameObject scoreDetails;
    
  

    public  Image darkscreen;
    private Color fadeblackaway;
    private Color finalstate;
    private Color makeitfadeaway;

    // Use this for initialization

    void Awake()
    {
        StartCoroutine(fadethepanelaway());
    }
    void Start()
    {
        scoreDetails = GameObject.Find("ScoreAndName");
        scoreDetails.SetActive(false);

        StartCoroutine(fadethepanelaway());
        changetransparency = instruction.color;
        gamestarted = false;
        tutorialdeact = false;
        wasScreenTapped = false;

        zoom = Camera.main.orthographicSize;
        posx = gameObject.transform.position.x;
        posy = gameObject.transform.position.y;

        initialcamerapos = Camera.main.orthographicSize;
        startingcolor.a = 0.0f;
        finalcolor.a = 1.0f;
        instruction.color = startingcolor;


        fadeblackaway.a = 1.0f;
        finalstate.a = 0.0f;
        darkscreen.color = fadeblackaway;




        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.Portrait;

        //google play service code

       

      

  
        // END THE CODE TO PASTE INTO START

     

        

    }




   
    public void onPress()
    {

        for (int x = 0; x < menuButtons.Count; x++)
        {
            menuButtons[x].transform.position = Vector3.MoveTowards(menuButtons[x].transform.position, new Vector3(menuButtons[x].transform.position.x, menuButtons[x].transform.position.y - 0.5f, menuButtons[x].transform.position.z), Time.deltaTime * 15);
            menuButtons[x].SetActive(false);
        }
        gamestarted = true;
    }

    /*   public float fadeOutTime;
       public void FadeOut()
       {
           StartCoroutine(FadeOutRoutine());
       }
       private IEnumerator FadeOutRoutine()
       {


           for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
           {
               instruction.color = Color.Lerp(Color.clear, finalcolor, Mathf.Min(1, t / fadeOutTime));
               yield return null;
           }
       }

       */


    public float time;
    IEnumerator changeopacity()
    {
        for (float x = 0; x < time; x +=Time.deltaTime)
        {
            changetransparency.a = Mathf.Lerp(startingcolor.a, finalcolor.a, Mathf.Min(1, x/time));
            instruction.color = changetransparency;
            yield return null;
        }
    }
    IEnumerator fadethepanelaway()
    {
      
        for(float y=0; y<1.0f; y += Time.deltaTime)
        {
            makeitfadeaway.a = Mathf.Lerp(fadeblackaway.a, finalstate.a, y*3.0f);
            darkscreen.color = makeitfadeaway;
          //  if (makeitfadeaway.a <= 0.1f)
            //{
              //  Destroy(darkscreen);
            //}
            yield return null;
        }
    }
    void fadethetextin()
    {
        StartCoroutine(changeopacity());
    }

    
    /// <summary>
    ///  Starts a game
    /// </summary>
    private void Initialized()
    {
      

      
        if (gamestarted)
        {
            //check if player is dead
            if (!Player.isDead)
            {
                if (!instruction.gameObject.activeInHierarchy)
                {
                    //  sm.scoreCounter.gameObject.SetActive(true);
                    scoreDetails.SetActive(true);

                }


            }
            else
            {
                scoreDetails.SetActive(false);
            }


            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSize, Time.deltaTime);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0, 0, gameObject.transform.position.z), Time.deltaTime);


            fadethetextin();
            if (Input.GetMouseButtonDown(0))
            {

                wasScreenTapped = true;
            }


            if (wasScreenTapped)
            {
                tutorialdeact = true;
                instruction.gameObject.SetActive(false);

                anim.SetBool("game started", true);

            }

        }
    }




    // Update is called once per frame
    void Update () 
    {
        
      
        Initialized();

    }
}
