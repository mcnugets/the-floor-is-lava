using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class proceduralgeneration : MonoBehaviour
{
    #region veriable Initialization

   
    public GameObject column;

    private GameObject startingPoint;

    private Vector3 movefrom;
    private Vector3 moveto;
    private float speed;
    private float pos;

    private float posBG;
    private float posBG2;
    private float lavaPos;
    public Camera maincamera;
    public Vector2 width;

    float camerawidth;
    float cameraHeight;

    float rigthSideofScreen;
    float leftSideofScreen;
    
    float timer;
    private float randomHeight;
    private float getTheOffset;
    private float waitforseconds;
    private static float reachRandomTime;

    private static GameObject[] columnHeight;
    public GameObject prefabCopy;
    private BoxCollider2D columnCollider;
    public static List<GameObject> queue;
    

    public GameObject[] environment;
   
    private static List<GameObject> bgWdith;
    private static List<GameObject> environment1;
    private static List<GameObject> environment2;
    private static List<GameObject> lavaenvironment;

    private static List<GameObject> groundGen;
    #region spriteWidth
    private float environmentWidth;
    private float secondenvironmentWdith;
    private float widthofthwlavasprite;
    private float groundWdith;
    #endregion

    #region scrollEnvironment
    private int scrollenvironment;
    private int scrollRearenvironment;
    private int scrollLavaAnimation;
    private int scrollGround;
    #endregion

    private IEnumerator myCroutine;
    private IEnumerator myCroutine2;
    private IEnumerator myCroutine3;
    #endregion
    //  public Vector3 height;

    // Use this for initialization
    void Start()
    {
        waitforseconds = 0f;
        environmentWidth = 0;
        secondenvironmentWdith = 0;
        widthofthwlavasprite = 0;
        columnHeight = new GameObject[9];
        reachRandomTime = 0;


        for (int x=0;x<columnHeight.Length; x++)
        {
            columnHeight[x] = prefabCopy;
        }



        speed = 5f * Time.deltaTime;
        scrollenvironment = 0;
        scrollRearenvironment = 0;
        width = maincamera.transform.localScale;



        


        bgWdith = new List<GameObject>();
        queue = new List<GameObject>();
        environment1 = new List<GameObject>();
        environment2 = new List<GameObject>();


        lavaenvironment = new List<GameObject>();
        timer = 2.25f;

        startingPoint = GameObject.FindGameObjectWithTag("starting point");

        //CAMERA COUROUTINE
        StartCoroutine(cameraUpdate());


        //PROCEDURAL GENERATION COUROUTINE


        StartCoroutine(columnGenerator());
        StartNewCroutine();
        StartAnotherCroutine();
        StartThirdCroutine();
     
        // columnGenerator();

    }
    #region start_coroutine_func

    
    private void StartNewCroutine()
    {
        myCroutine = environmentgenerator();
        StartCoroutine(myCroutine);
    }
    private void StartAnotherCroutine()
    {
        myCroutine2 = rearBackgound();
        StartCoroutine(myCroutine2);
    }
    private void StartThirdCroutine()
    {
        myCroutine3 = Lavagenerator();
        StartCoroutine(myCroutine3);
    }
    #endregion

    #region environmentIEnumenator


    IEnumerator cameraUpdate()
    {
        while (true)
        {
            cameraHeight = maincamera.orthographicSize;
            camerawidth = maincamera.aspect * cameraHeight;
            rigthSideofScreen = camerawidth;
            leftSideofScreen = -camerawidth;

        

            yield return null;
        }

     
    }
    IEnumerator environmentgenerator()
    {
      
      

       

        while (scrollenvironment < 2)
        {


            GameObject instantiateenvironment = Instantiate(environment[0], new Vector2(environment[0].transform.position.x, environment[0].transform.position.y), Quaternion.identity);
            environment1.Add(instantiateenvironment);
            environment1[scrollenvironment].transform.position = new Vector2(environmentWidth, environment1[scrollenvironment].transform.position.y);

            scrollenvironment++;


            yield return null;
        }
 

    }


    IEnumerator rearBackgound()
    {
        while (scrollRearenvironment < 2)
        {


            GameObject instantiateenvironment = Instantiate(environment[1], new Vector2(environment[1].transform.position.x, environment[1].transform.position.y), Quaternion.identity);
            environment2.Add(instantiateenvironment);
            environment2[scrollRearenvironment].transform.position = new Vector2(secondenvironmentWdith, environment2[scrollRearenvironment].transform.position.y);

            scrollRearenvironment++;


            yield return null;
        }
    }

    // IENUMENATOR FOR A LAVA ANIMIATUIOINS

    IEnumerator Lavagenerator()
    {
        while (scrollLavaAnimation < 3)
        {
            GameObject instantiateLavaAnimation = Instantiate(environment[2], new Vector2(environment[2].transform.position.x, environment[2].transform.position.y), Quaternion.identity);
            lavaenvironment.Add(instantiateLavaAnimation);
            lavaenvironment[scrollLavaAnimation].transform.position = new Vector2(widthofthwlavasprite, lavaenvironment[scrollLavaAnimation].transform.position.y);

            scrollLavaAnimation++;
            yield return null;
        }
    }
    IEnumerator groundGenerator()
    {
        while (scrollenvironment < 2)
        {
            GameObject instantiate = Instantiate(environment[3], new Vector2(environment[3].transform.position.x, environment[3].transform.position.y), Quaternion.identity);
            groundGen.Add(instantiate);
            groundGen[scrollenvironment].transform.position = new Vector2(secondenvironmentWdith, environment2[scrollRearenvironment].transform.position.y);
        }
    }

    // IENUMENATOR FOR A COLUMN
    IEnumerator columnGenerator()
    {
      
        
        while (!Player.isDead)
        {



          
           

            if (FitDatCamera.wasScreenTapped)
            {

                
           

              //  if (timer<=0)
            //    {

                    randomHeight = Mathf.Round(RandomNum(1f, 9f));
                  
                    // getTheOffset = randomHeight - 0.5f;



                    GameObject instanceParent = Instantiate(column, new Vector2(rigthSideofScreen + 1.0f, column.transform.position.y), Quaternion.identity);
                    columnCollider = instanceParent.GetComponent<BoxCollider2D>();
                    for (int x = 0; x < randomHeight; x++)
                    {

                        getTheOffset = (randomHeight * 0.5f) - 0.5f;

                      
                        

                        GameObject instanceChild = Instantiate(columnHeight[x], new Vector2(columnHeight[x].transform.localPosition.x, columnHeight[x].transform.localPosition.y), Quaternion.identity);

                        instanceChild.transform.parent = instanceParent.transform;
                        instanceChild.transform.localPosition = new Vector2(columnHeight[x].transform.localPosition.x, columnHeight[x].transform.localPosition.y + x);
                        columnCollider.size = new Vector2(columnCollider.size.x, randomHeight);
                        columnCollider.offset = new Vector2(columnCollider.offset.x, getTheOffset);
                    }

                    queue.Add(instanceParent);




                //  }
                reachRandomTime = RandomNum(0.50f, 2.50f);
                Debug.Log("REACH RANOMD TIME " + reachRandomTime);
                yield return new WaitForSeconds(reachRandomTime);


            }
            
            yield return null;
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
     
        while (!Player.isDead)

        {

            if (FitDatCamera.wasScreenTapped)
            {
                //LAVA SCROLLING 

                for (int x = 0; x < lavaenvironment.Count; x++)
                {
                    Vector2[] movingLava = new Vector2[lavaenvironment.Count];
                    if (lavaenvironment[x].transform.position.x > leftSideofScreen)
                    {
                        lavaPos -= 0.5f;
                    }

                    movingLava[x] = new Vector3(lavaPos, lavaenvironment[x].transform.position.y, lavaenvironment[x].transform.position.z);
                    lavaenvironment[x].transform.position = Vector3.MoveTowards(lavaenvironment[x].transform.position, movingLava[x], 3f * Time.deltaTime);

                    if (lavaenvironment[x].transform.position.x + lavaenvironment[x].GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {
                        Destroy(lavaenvironment[x]);
                        lavaenvironment.RemoveAt(x);
                        scrollLavaAnimation = 2;

                        StopCoroutine(myCroutine3);
                        StartThirdCroutine();


                    }

                }


                //FRONT environment 
                for (int x = 0; x < environment1.Count; x++)
                {
                    Vector2[] movesuka2 = new Vector2[environment1.Count];
                    if (environment1[x].transform.position.x > leftSideofScreen)
                    {
                        posBG -= 0.5f;

                    }
                    Debug.Log(environment1[x].GetComponent<SpriteRenderer>().bounds.size.x);

                    movesuka2[x] = new Vector3(posBG, environment1[x].transform.position.y, environment1[x].transform.position.z);
                    environment1[x].transform.position = Vector3.MoveTowards(environment1[x].transform.position, movesuka2[x], 3f * Time.deltaTime);

                    if (environment1[x].transform.position.x + environment1[x].GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {


                        Destroy(environment1[x]);
                        environment1.RemoveAt(x);
                        scrollenvironment = 1;

                        StopCoroutine(myCroutine);
                        StartNewCroutine();

                    }
                  
                }

                //REAR environment
                for (int x = 0; x < environment2.Count; x++)
                {
                    //CHECK THIS FOR LATER <<<<<<<<<<<<<<<
                    Vector2[] movesuka3 = new Vector2[environment2.Count];
                    if (environment2[x].transform.position.x > leftSideofScreen)
                    {
                        posBG2 -= 0.1f;
                    }
                    Debug.Log(environment2[x].GetComponent<SpriteRenderer>().bounds.size.x);

                    movesuka3[x] = new Vector3(posBG2, environment2[x].transform.position.y, environment2[x].transform.position.z);
                    environment2[x].transform.position = Vector3.MoveTowards(environment2[x].transform.position, movesuka3[x], 2f * Time.deltaTime);

                    if (environment2[x].transform.position.x + environment2[x].GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {

                            
                        Destroy(environment2[x]);
                        environment2.RemoveAt(x);
                        scrollRearenvironment = 1;

                        StopCoroutine(myCroutine2);
                        StartAnotherCroutine();

                    }

                }



                //COLUMNS
                for (int x = 0; x < queue.Count; x++)
                {

                    Vector3[] movesuka = new Vector3[queue.Count];
                    // Debug.Log(leftSideofScreen);
                    if (queue[x].transform.position.x > leftSideofScreen)
                    {

                        Vector3 screenCneter = maincamera.WorldToScreenPoint(queue[x].transform.position);

                        pos -= 0.5f;


                    }
                    //Debug.Log(x);
                    movesuka[x] = new Vector3(pos, queue[x].transform.position.y, queue[x].transform.position.z);
                    queue[x].transform.position = Vector3.MoveTowards(queue[x].transform.position, movesuka[x], 3f * Time.deltaTime);

                   // queue[x].transform.eulerAngles = new Vector3(queue[x].transform.eulerAngles.x, queue[x].transform.eulerAngles.y, queue[x].transform.eulerAngles.z);
                   // Debug.Log(queue[x].transform.eulerAngles.z );
                 

                    if (queue[x].transform.position.x + 1f < leftSideofScreen || queue[x].transform.eulerAngles.z > 45.0f && queue[x].transform.eulerAngles.z < 315.0f)
                    {


                        Destroy(queue[x]);
                        queue.RemoveAt(x);


                    }
                   

                }

                if (startingPoint != null)
                {

                    if (startingPoint.transform.position.x + startingPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {

                        Destroy(startingPoint);
                    }
                    startingPoint.transform.position = Vector3.MoveTowards(startingPoint.transform.position, new Vector3(pos, startingPoint.transform.position.y, startingPoint.transform.position.z), 3f * Time.deltaTime);


                }
                else
                {

                }

            }
            environmentWidth = environment1[scrollenvironment - 1].transform.position.x + environment1[scrollenvironment - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            secondenvironmentWdith = environment2[scrollRearenvironment - 1].transform.position.x + environment2[scrollRearenvironment - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            widthofthwlavasprite = lavaenvironment[scrollLavaAnimation - 1].transform.position.x + lavaenvironment[scrollLavaAnimation - 1].GetComponent<SpriteRenderer>().bounds.size.x;

          

            break;
        }

        


        /*     Vector3 screenCneter = maincamera.WorldToScreenPoint(column.transform.position);


             if (screenCneter.x>width.x)
             {

                 pos -=0.1f;


             }
             else if (screenCneter.x < width.x)
             {
                 Destroy(column);
             }
             moveto = new Vector3(pos, column.transform.position.y, column.transform.position.z);
             column.transform.position = Vector3.MoveTowards(column.transform.position, moveto, 1f * Time.deltaTime);
         */
    }
    public float RandomNum(float min, float max)
    {
        float value = Random.Range(min, max);
        
        return value;


    }
}
