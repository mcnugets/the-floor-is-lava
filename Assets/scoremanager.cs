using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour {

    private static int lastscorerecorded;


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
       

    }

  
    // Update is called once per frame
    void Update () {

        

        scoreCounter.text = Player.score.ToString();
        
     
        if (Player.isDead)
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

        GooglePlayManager.scoreUpdate(GPGSIds.leaderboard_score, Player.score);
        Player.score = 0;
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
