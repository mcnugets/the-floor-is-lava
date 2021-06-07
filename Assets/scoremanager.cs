using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour {

    private static int lastscorerecorded;
    public static bool isDead;

    public Text scoreCounter;
    public Text highestScore;
    public  List<GameObject> gameoverUI;
    public characterController2D charactercontroller;
    
    float elaspedTime = 0;
   
    float timer = 1f;
    public static bool gameover;
    

	// Use this for initialization
	void Start () {
        gameover = false;
        isDead = false;

    }

  
    // Update is called once per frame
    void Update () {

        

        scoreCounter.text = characterController2D.numberoflanding.ToString();
     
        if (isDead)
        {
                timer -= Time.deltaTime;
           
 
                if (timer <= 0)
                {
                    GameOver();
                }   
        }


    }
    void GameOver()
    {
        gameover = true;
       
        lastscorerecorded = characterController2D.numberoflanding;
        highestScore.text = lastscorerecorded.ToString();
        for (int y = 0; y < gameoverUI.Count; y++)
        {
            gameoverUI[y].SetActive(true);
        }

        elaspedTime += Time.deltaTime;
        gameoverUI[0].transform.position = Vector3.Lerp(gameoverUI[0].transform.position, new Vector2(gameoverUI[0].transform.position.x, Screen.height / 2f), elaspedTime);
        gameoverUI[1].transform.position = Vector3.Lerp(gameoverUI[1].transform.position, new Vector2(gameoverUI[1].transform.position.x, Screen.height / 1.25f), elaspedTime);

    }
}
