using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class proceduralgeneration : MonoBehaviour
{

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
    

    public GameObject[] background;
   
    private static List<GameObject> bgWdith;
    private static List<GameObject> background1;
    private static List<GameObject> background2;
    private static List<GameObject> lavabackground;

    private float backgroundWidth;
    private float secondbackgroundWdith;
    private float widthofthwlavasprite;
    private int scrollBackground;
    private int scrollRearBackground;
    private int scrollLavaAnimation;
    private IEnumerator myCroutine;
    private IEnumerator myCroutine2;
    private IEnumerator myCroutine3;
  
    //  public Vector3 height;

    // Use this for initialization
    void Start()
    {
        waitforseconds = 0f;
        backgroundWidth = 0;
        secondbackgroundWdith = 0;
        widthofthwlavasprite = 0;
        columnHeight = new GameObject[9];
        reachRandomTime = 0;


        for (int x=0;x<columnHeight.Length; x++)
        {
            columnHeight[x] = prefabCopy;
        }



        speed = 5f * Time.deltaTime;
        scrollBackground = 0;
        scrollRearBackground = 0;
        width = maincamera.transform.localScale;



        


        bgWdith = new List<GameObject>();
        queue = new List<GameObject>();
        background1 = new List<GameObject>();
        background2 = new List<GameObject>();
        lavabackground = new List<GameObject>();
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
    private void StartNewCroutine()
    {
        myCroutine = backgroundgenerator();
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
    IEnumerator backgroundgenerator()
    {
      
      

       

        while (scrollBackground < 2)
        {


            GameObject instantiateBackground = Instantiate(background[0], new Vector2(background[0].transform.position.x, background[0].transform.position.y), Quaternion.identity);
            background1.Add(instantiateBackground);
            background1[scrollBackground].transform.position = new Vector2(backgroundWidth, background1[scrollBackground].transform.position.y);

            scrollBackground++;


            yield return null;
        }
 

    }

    IEnumerator rearBackgound()
    {
        while (scrollRearBackground < 2)
        {


            GameObject instantiateBackground = Instantiate(background[1], new Vector2(background[1].transform.position.x, background[1].transform.position.y), Quaternion.identity);
            background2.Add(instantiateBackground);
            background2[scrollRearBackground].transform.position = new Vector2(secondbackgroundWdith, background2[scrollRearBackground].transform.position.y);

            scrollRearBackground++;


            yield return null;
        }
    }

    // IENUMENATOR FOR A LAVA ANIMIATUIOINS

    IEnumerator Lavagenerator()
    {
        while (scrollLavaAnimation < 3)
        {
            GameObject instantiateLavaAnimation = Instantiate(background[2], new Vector2(background[2].transform.position.x, background[2].transform.position.y), Quaternion.identity);
            lavabackground.Add(instantiateLavaAnimation);
            lavabackground[scrollLavaAnimation].transform.position = new Vector2(widthofthwlavasprite, lavabackground[scrollLavaAnimation].transform.position.y);

            scrollLavaAnimation++;
            yield return null;
        }
    }

    // IENUMENATOR FOR A COLUMN
    IEnumerator columnGenerator()
    {
      
        
        while (!scoremanager.isDead)
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
  

    // Update is called once per frame
    void Update()
    {
        
     
        while (!scoremanager.isDead)

        {

            if (FitDatCamera.wasScreenTapped)
            {
                //LAVA SCROLLING 

                for (int x = 0; x < lavabackground.Count; x++)
                {
                    Vector2[] movingLava = new Vector2[lavabackground.Count];
                    if (lavabackground[x].transform.position.x > leftSideofScreen)
                    {
                        lavaPos -= 0.5f;
                    }

                    movingLava[x] = new Vector3(lavaPos, lavabackground[x].transform.position.y, lavabackground[x].transform.position.z);
                    lavabackground[x].transform.position = Vector3.MoveTowards(lavabackground[x].transform.position, movingLava[x], 3f * Time.deltaTime);

                    if (lavabackground[x].transform.position.x + lavabackground[x].GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {
                        Destroy(lavabackground[x]);
                        lavabackground.RemoveAt(x);
                        scrollLavaAnimation = 2;

                        StopCoroutine(myCroutine3);
                        StartThirdCroutine();


                    }

                }


                //FRONT BACKGROUND 
                for (int x = 0; x < background1.Count; x++)
                {
                    Vector2[] movesuka2 = new Vector2[background1.Count];
                    if (background1[x].transform.position.x > leftSideofScreen)
                    {
                        posBG -= 0.5f;

                    }
                    Debug.Log(background1[x].GetComponent<SpriteRenderer>().bounds.size.x);

                    movesuka2[x] = new Vector3(posBG, background1[x].transform.position.y, background1[x].transform.position.z);
                    background1[x].transform.position = Vector3.MoveTowards(background1[x].transform.position, movesuka2[x], 3f * Time.deltaTime);

                    if (background1[x].transform.position.x + background1[x].GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {


                        Destroy(background1[x]);
                        background1.RemoveAt(x);
                        scrollBackground = 1;

                        StopCoroutine(myCroutine);
                        StartNewCroutine();

                    }
                  
                }

                //REAR BACKGROUND
                for (int x = 0; x < background2.Count; x++)
                {
                    //CHECK THIS FOR LATER <<<<<<<<<<<<<<<
                    Vector2[] movesuka3 = new Vector2[background2.Count];
                    if (background2[x].transform.position.x > leftSideofScreen)
                    {
                        posBG2 -= 0.1f;
                    }
                    Debug.Log(background2[x].GetComponent<SpriteRenderer>().bounds.size.x);

                    movesuka3[x] = new Vector3(posBG2, background2[x].transform.position.y, background2[x].transform.position.z);
                    background2[x].transform.position = Vector3.MoveTowards(background2[x].transform.position, movesuka3[x], 2f * Time.deltaTime);

                    if (background2[x].transform.position.x + background2[x].GetComponent<SpriteRenderer>().bounds.size.x / 2 < leftSideofScreen)
                    {

                            
                        Destroy(background2[x]);
                        background2.RemoveAt(x);
                        scrollRearBackground = 1;

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
            backgroundWidth = background1[scrollBackground - 1].transform.position.x + background1[scrollBackground - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            secondbackgroundWdith = background2[scrollRearBackground - 1].transform.position.x + background2[scrollRearBackground - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            widthofthwlavasprite = lavabackground[scrollLavaAnimation - 1].transform.position.x + lavabackground[scrollLavaAnimation - 1].GetComponent<SpriteRenderer>().bounds.size.x;

          

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
