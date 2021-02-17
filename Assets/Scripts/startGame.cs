using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGame : MonoBehaviour {

   // public static Button buttonStart;
    public  GameObject scoretext;
    public Animator animator;

	// Use this for initialization
	void Start () {
        Time.timeScale=0;
        scoretext.SetActive(false);
	}
   public void OnClick()
    {
        Time.timeScale = 1;
        scoretext.SetActive(true);
        gameObject.SetActive(false);
        animator.SetBool("game started", true);
    }
	// Update is called once per frame
	void Update () {
		
	}
    void Pause()
    {
        
    }
}
