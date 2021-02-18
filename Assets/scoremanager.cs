using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour {

    public Text scoreCounter;
    public Text highestScore;
    public  List<GameObject> gameoverUI;
    public characterController2D charactercontroller;
    private static int lastscorerecorded;
    float elaspedTime = 0;
    private List<RectTransform> testing;
    float timer = 1f;
    public static bool gameover;
    

	// Use this for initialization
	void Start () {
        gameover = false;
        testing = new List<RectTransform>();
        // testing = gameoverUI.GetComponent<RectTransform>();
        for (int suka = 0; suka < gameoverUI.Count; suka++)
        {
            testing.Add(gameoverUI[suka].GetComponent<RectTransform>());
        }
    }

  
    // Update is called once per frame
    void Update () {
        scoreCounter.text = characterController2D.numberoflanding.ToString();
       
        if (Player.isDead())
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
        testing[0].transform.position = Vector3.Lerp(testing[0].transform.position, new Vector2(testing[0].transform.position.x, Screen.height / 2f), elaspedTime);
        testing[1].transform.position = Vector3.Lerp(testing[1].transform.position, new Vector2(testing[1].transform.position.x, Screen.height / 1.25f), elaspedTime);

    }
}
