using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class scoremanager : MonoBehaviour
{

    private static int lastscorerecorded;


    public Text scoreCounter;
    public Text highestScore;
    public List<GameObject> gameoverUI;
    public characterController2D charactercontroller;

    float elaspedTime = 0;
    private float wait_time = 0;
    float timer = 1f;
    public static bool gameover;

    Player.Player_Details get;

    

    // Use this for initialization
    void Start()
    {
        gameover = false;
        get = new Player.Player_Details();




    }


    // Update is called once per frame
    void Update()
    {


        if (10 > get.player_details.Score) scoreCounter.text = "00" + get.player_details.Score.ToString();
        else if (99 >= get.player_details.Score) scoreCounter.text = "0" + get.player_details.Score.ToString();
        else if (99 < get.player_details.Score) scoreCounter.text = get.player_details.Score.ToString();



        if (Player.isDead)
        {

            timer -= Time.deltaTime;


            if (timer <= 0 && !gameover)
            {

                GameOver();
                gameover = !gameover;
            }
        }
        else if (FitDatCamera.gamestarted)
        {


            if (FitDatCamera.tutorialdeact)
            {
                //touch screen contorlls

                DistanceMade();

            }




        }


    }
    private void DistanceMade()
    {

        wait_time += Time.deltaTime * 2f;
        if (wait_time >= 1)
        {
            get.player_details.Score++;
            wait_time = 0;
        }



    }
    void GameOver()
    {
      

        GooglePlayManager.scoreUpdate(GPGSIds.leaderboard_score, get.player_details.Score);
   

        DataParse.Save(get);
        get.player_details.Score = 0;
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

