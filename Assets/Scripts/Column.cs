using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
    int check = 0;
    int listconuter = 0;
    private Player player;
   
    Vector3 initialScale;
    private bool intheair;
    private Vector3 orgpos;
  
    float yvelocitt = 0.0f;
    Vector3 curScale;
    public int scaleCounter = 0;
    public int sukaCheck = 0;
    public int randomshit;
    public float randomshit12;

    static List<int> storedValues;
    private int x;
    private int y;
    private int difference;
    int[] randomarrayNum;
    List<Vector3> targetScale;

    // Use this for initialization

    IEnumerator wait(float secondss)
    {
        yield return new WaitForSecondsRealtime(secondss);
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        initialScale = transform.localScale;
        x = 4;
        y = 9;
        difference = y - x;
        storedValues = new List<int>();

        

        randomshit = RandomNum(1, 9);
        transform.localScale = new Vector3(transform.localScale.x, randomshit, transform.localScale.z);

    }

    // Update is called once per frames
    void FixedUpdate() {


    


        /*  //   else
          //   {

           //  }

             //  float newposition = Mathf.SmoothDamp(transform.localScale.z, Random.Range(4.0f, 9.0f), ref yvelocitt, Time.deltaTime);







             //  new Vector3(gameObject.transform.localScale.x, Random.Range(6.0f, 8.0f), gameObject.transform.localScale.z);
             */
    }
    
    public void scaleDatShit()
    {

    }

    public void ScaleHeight()
    {

        
       
            if (listconuter < targetScale.Count)
            {
                
                curScale = Vector3.Lerp(initialScale, targetScale[listconuter], 15* Time.deltaTime);
                transform.localScale = curScale;
                


            }
             
       
      
        


        /*           // scaleCounter = 0;

               }
           }
       }*/



    }



    public int RandomNum(int min, int max)
    {
        int value = Random.Range(min, max);
        return value;

    }
}
