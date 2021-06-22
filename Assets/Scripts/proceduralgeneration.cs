using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class proceduralgeneration : MonoBehaviour
{
    #region veriable Initialization

    #region Mono
    public GameObject column;
    private GameObject startingPoint;
    private static GameObject[] columnHeight;
    public GameObject prefabCopy;
    private BoxCollider2D columnCollider;
    private Vector3 movefrom;
    private Vector3 moveto;
    public Camera maincamera;
    public Vector2 width;
    #endregion

    #region floats
    private float environmentWidth;
    private float secondenvironmentWdith;
    private float widthofthwlavasprite;
    private float groundWdith;

    float camerawidth;
    float cameraHeight;

    float rigthSideofScreen;
    float leftSideofScreen;

    float timer;
    private float randomHeight;
    private float getTheOffset;
    private float waitforseconds;
    private static float reachRandomTime;

    private float speed;
    private float pos;
    #endregion

    #region data sctructure 
    private int[] index_to_gameObject;
    public GameObject[] environment;
    private static List<GameObject> bgWdith;
    private static List<GameObject> front_bg_list;
    private static List<GameObject> rear_bg_list;
    private static List<GameObject> lavaenvironment;
    public static List<GameObject> queue;
    private static List<GameObject> groundGen;
    private float[] sprite_pos;
    #endregion

    #region IEnumerators
    private IEnumerator myCroutine;
    private IEnumerator myCroutine2;
    private IEnumerator myCroutine3;
    private IEnumerator myCroutine4;
    #endregion


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
      
        width = maincamera.transform.localScale;





        sprite_pos = new float[4];
        index_to_gameObject = new int[4];
        bgWdith = new List<GameObject>();
        queue = new List<GameObject>();
        front_bg_list = new List<GameObject>();
        rear_bg_list = new List<GameObject>();
        groundGen = new List<GameObject>();
        lavaenvironment = new List<GameObject>();
        timer = 2.25f;

        startingPoint = GameObject.FindGameObjectWithTag("land");

        //CAMERA COUROUTINE
        StartCoroutine(cameraUpdate());


        //PROCEDURAL GENERATION COUROUTINE


        StartFirstCoroutine();
        StartSecondCourutine();
        StartThirdCroutine();
        StartForthCoroutine();
        StartCoroutine(columnGenerator());

    }
    #region start_coroutine_func

    
    private void StartFirstCoroutine()
    {
        myCroutine = frontBackground(0);
        StartCoroutine(myCroutine);
    }
    private void StartSecondCourutine()
    {
        myCroutine2 = rearBackgound(1);
        StartCoroutine(myCroutine2);
    }
    private void StartThirdCroutine()
    {
        myCroutine3 = Lavagenerator(2);
        StartCoroutine(myCroutine3);
    }
    private void StartForthCoroutine() 
    {
        myCroutine4 = groundGenerator(3);
        StartCoroutine(myCroutine4);
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
    /// <summary>
    /// numerator for the front bakground sprite
    /// </summary>
    /// <param name="i"> index 0 </param>
    /// <returns></returns>
    IEnumerator frontBackground(int i)
    {
      

        while (index_to_gameObject[i] < 2)
        {


            GameObject instantiateenvironment = Instantiate(environment[i], new Vector2(environment[i].transform.position.x, environment[i].transform.position.y), Quaternion.identity);
            front_bg_list.Add(instantiateenvironment);
            front_bg_list[index_to_gameObject[i]].transform.position = new Vector2(environmentWidth, front_bg_list[index_to_gameObject[i]].transform.position.y);

            index_to_gameObject[i]++;
            Debug.Log("INDEX TO THE SHIT==> " + index_to_gameObject[i]);

            yield return null;
        }
 

    }

    /// <summary>
    /// numerator for the rear bakground sprite
    /// </summary>
    /// <param name="i"> index 1 </param>
    /// <returns></returns>
    IEnumerator rearBackgound(int i)
    {
        while (index_to_gameObject[i] < 2)
        {


            GameObject instantiateenvironment = Instantiate(environment[i], new Vector2(environment[i].transform.position.x, environment[i].transform.position.y), Quaternion.identity);
            rear_bg_list.Add(instantiateenvironment);
            rear_bg_list[index_to_gameObject[i]].transform.position = new Vector2(secondenvironmentWdith, rear_bg_list[index_to_gameObject[i]].transform.position.y);

            index_to_gameObject[i]++;


            yield return null;
        }
    }

    // IENUMENATOR FOR A LAVA ANIMIATUIOINS

    /// <summary>
    /// numerator for the lava sprite
    /// </summary>
    /// <param name="i"> index 2 </param>
    /// <returns></returns>

    IEnumerator Lavagenerator(int i)
    {
        while (index_to_gameObject[i] < 3)
        {
            GameObject instantiateLavaAnimation = Instantiate(environment[i], new Vector2(environment[i].transform.position.x, environment[i].transform.position.y), Quaternion.identity);
            lavaenvironment.Add(instantiateLavaAnimation);
            lavaenvironment[index_to_gameObject[i]].transform.position = new Vector2(widthofthwlavasprite, lavaenvironment[index_to_gameObject[i]].transform.position.y);

            index_to_gameObject[i]++;
            yield return null;
        }
    }


    /// <summary>
    /// numerator for the land sprite
    /// </summary>
    /// <param name="i"> index 3 </param>
    /// <returns></returns>

    IEnumerator groundGenerator(int i)
    {

        while (index_to_gameObject[i] < 3)
        {
            GameObject instantiate = Instantiate(environment[i], new Vector2(environment[i].transform.position.x, environment[i].transform.position.y), Quaternion.identity);
            groundGen.Add(instantiate);
            groundGen[index_to_gameObject[i]].transform.position = new Vector2(groundWdith, groundGen[index_to_gameObject[i]].transform.position.y);
            index_to_gameObject[i]++;
            yield return null;
        }
    }

    // IENUMENATOR FOR A COLUMN
    IEnumerator columnGenerator()
    {


        while (!Player.isDead)
        {


            if (FitDatCamera.wasScreenTapped)
            {

                randomHeight = Mathf.Round(RandomNum(2f, 5f));


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





                reachRandomTime = RandomNum(1.50f, 3.50f);
                yield return new WaitForSeconds(reachRandomTime);


            }

            yield return null;
        }
    }
    #endregion

/// <summary>
/// makes scrolling animation using multiple sprites 
/// </summary>
/// <param name="scroll_sprite"> list of sprites</param>
/// <param name="reset_i"> reset the animation for scrolling</param>
/// <param name="index"> value that used to index element from list </param>
/// <param name="scrollSpeed">sets the scroll speed</param>
/// <param name="coroutine">numerator restarts as soon as sprite is out of the FOV</param>
/// <param name="StartCoroutine"></param>
    private void environment_manager(List<GameObject> scroll_sprite, int reset_i, int index,float scrollSpeed, IEnumerator coroutine, float speed, UnityAction StartCoroutine)
    {
        for (int x = 0; x < scroll_sprite.Count; x++)
        {
            Vector2[] movingLava = new Vector2[scroll_sprite.Count];
            if (scroll_sprite[x].transform.position.x > leftSideofScreen)
            {
                sprite_pos[index] -= scrollSpeed;
              
            }

            movingLava[x] = new Vector3(sprite_pos[index], scroll_sprite[x].transform.position.y, scroll_sprite[x].transform.position.z);
            scroll_sprite[x].transform.position = Vector3.MoveTowards(scroll_sprite[x].transform.position, movingLava[x], speed * Time.deltaTime);
            float sprite_size = scroll_sprite[x].transform.position.x + scroll_sprite[x].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            if (sprite_size < leftSideofScreen)
            {
                

                Destroy(scroll_sprite[x]);
                scroll_sprite.RemoveAt(x);
                index_to_gameObject[index] = reset_i;
                StopCoroutine(coroutine);
                StartCoroutine();
                sprite_pos[index] = 0;

            }


        }
        
    }
    // Update is called once per frame
    void Update()
    {
      

        while (!Player.isDead)

        {

            if (FitDatCamera.wasScreenTapped)
            {
               

                //FRONT BACKGROUND SCROLLING
                environment_manager(front_bg_list, 1, 0, 0.5f, myCroutine, 3f, StartFirstCoroutine);

                //REAR BACKGROUND SCROLLING
                environment_manager(rear_bg_list, 1, 1, 0.1f, myCroutine2, 2f, StartSecondCourutine);

                //LAVA SCROLLING 
                environment_manager(lavaenvironment, 2, 2, 0.5f, myCroutine3, 3f, StartThirdCroutine);

                //GROUND SCROLLLING    
                environment_manager(groundGen, 2, 3, 0.5f, myCroutine4, 3f, StartForthCoroutine);

              



                //COLUMNS
                   for (int x = 0; x < queue.Count; x++)
                   {

                       Vector3[] move_column = new Vector3[queue.Count];
                       // Debug.Log(leftSideofScreen);
                       if (queue[x].transform.position.x > leftSideofScreen)
                       {

                           Vector3 screenCneter = maincamera.WorldToScreenPoint(queue[x].transform.position);

                           pos -= 0.5f;


                       }
                       //Debug.Log(x);
                     

                       move_column[x] = new Vector3(pos, queue[x].transform.position.y, queue[x].transform.position.z);
                       queue[x].transform.position = Vector3.MoveTowards(queue[x].transform.position, move_column[x], 3f * Time.deltaTime);

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
                    if (startingPoint.transform.position.x > leftSideofScreen)
                    {



                        pos -= 0.5f;


                    }

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
            environmentWidth = front_bg_list[index_to_gameObject[0] - 1].transform.position.x + front_bg_list[index_to_gameObject[0] - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            secondenvironmentWdith = rear_bg_list[index_to_gameObject[1] - 1].transform.position.x + rear_bg_list[index_to_gameObject[1] - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            widthofthwlavasprite = lavaenvironment[index_to_gameObject[2] - 1].transform.position.x + lavaenvironment[index_to_gameObject[2] - 1].GetComponent<SpriteRenderer>().bounds.size.x;

            groundWdith = groundGen[index_to_gameObject[3] - 1].transform.position.x + groundGen[index_to_gameObject[3] - 1].GetComponent<SpriteRenderer>().bounds.size.x;



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
        float value = UnityEngine.Random.Range(min, max);
        
        
        return value;


    }
}
