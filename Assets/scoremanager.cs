using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GooglePlayGames.BasicApi;

[Serializable]
public class scoremanager : MonoBehaviour
{



    #region UnityEngine
 
  
    [Header("Score UI")]
    public Text scoreCounter;
    public Text yourscore;
    public Text highestScore;
    [Header("Name UI")]
    public GameObject inputName;
    public Text UserName;
    [Header("Objects")]
    
    public List<GameObject> gameoverUI;
    public characterController2D charactercontroller;
    #endregion
   
    public static bool gameover;
    public bool isnameSet;
    #region private fields
    private float elaspedTime = 0;
    private float timer = 1f;
    private Player.Player_Details details;
    #endregion


    // Use this for initialization
    void Start()
    {
        gameover = false;
        details = new Player.Player_Details();
        DataParse.Load(details);

        if (string.IsNullOrEmpty(details.player_details.Name)) 
        {
            isnameSet = false;
            UserName.gameObject.SetActive(false);
        }
        else 
        {
            isnameSet = true;
            UserName.gameObject.SetActive(true);
            UserName.gameObject.transform.GetChild(0).GetComponent<Text>().text = details.player_details.Name.ToString();
        }
   

    }


    // Update is called once per frame
    void Update()
    {


        if (10 > Player.Score) scoreCounter.text = "00" + Player.Score.ToString();
        else if (99 >= Player.Score) scoreCounter.text = "0" + Player.Score.ToString();
        else if (99 < Player.Score) scoreCounter.text = Player.Score.ToString();



        if (Player.isDead)
        {

            timer -= Time.deltaTime;


            if (timer <= 0)
            {
                if (!gameover)
                {
                    SetBestScore();
                    gameover = !gameover;
                }
                GameOver();

            }
        }
   

    }

    public void SetYourName() 
    {
        InputField input = inputName.transform.GetChild(0).GetComponent<InputField>();
        details.player_details.Name = input.text;
        print(input.text);
        DataParse.Save(details);
    }

    void SetBestScore() 
    {
        if (isnameSet)
            inputName.SetActive(false);
#if UNITY_ANDROID
        GooglePlayManager.scoreUpdate(GPGSIds.leaderboard_score, details.player_details.Score);
#endif
        details.player_details.Date = DateTime.Now.ToString();
        if (Player.Score >= details.player_details.Score)
            details.player_details.Score = Player.Score;

        DataParse.Save(details);
        highestScore.text = details.player_details.Score.ToString();
        yourscore.text = Player.Score.ToString();
        Player.Score = 0;
       
    }
    void GameOver()
    {
      

        
        for (int y = 0; y < gameoverUI.Count; y++)
        {
            gameoverUI[y].SetActive(true);
        }

        elaspedTime += Time.deltaTime;
        gameoverUI[0].transform.position = Vector3.Lerp(gameoverUI[0].transform.position, new Vector2(gameoverUI[0].transform.position.x, Screen.height / 2f), elaspedTime);
        gameoverUI[1].transform.position = Vector3.Lerp(gameoverUI[1].transform.position, new Vector2(gameoverUI[1].transform.position.x, Screen.height / 1.25f), elaspedTime);

    }
}

