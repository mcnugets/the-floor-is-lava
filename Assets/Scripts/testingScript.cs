using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour {

    public GameObject a;
    float cehck;
    //public GameObject b;

  
    // Use this for initialization
    void Start () {

        cehck = 0;



    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            cehck=+0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cehck = -0.1f;
        }
        a.transform.eulerAngles = new Vector3(a.transform.eulerAngles.x, a.transform.eulerAngles.y, a.transform.eulerAngles.z + cehck);
        Debug.Log(a.transform.eulerAngles.z);
        if (a.transform.eulerAngles.z > 45.0f && a.transform.eulerAngles.z < 315)
        {
            Debug.Log(" THE OBJECT IS CURRNELTY IN THIS 45-315 ANGLE RANGE  ");
        }
    }
}
