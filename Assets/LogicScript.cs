using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LogicScript : MonoBehaviour
{
    public int bricksLeft;
    public int GameOverSceneNum;
    public int BrickKey;
    public int HealthPoints;
    public int currentLevelNo;
    GameObject Ball;
    GameObject Platform;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        Platform = GameObject.Find("Platform");
        Ball = GameObject.Find("Ball");
        ball = Ball.GetComponent<Ball>();
        DontDestroyOnLoad(this.gameObject);
        currentLevelNo++;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevelNo ==GameOverSceneNum)
        { 
             DestroyObjects();
        }
        if (HealthPoints == 0)
        {
            SceneManager.LoadScene(GameOverSceneNum);
            currentLevelNo = GameOverSceneNum;
            Debug.Log("a");
        }
        if (bricksLeft == 0)
        {
            if (currentLevelNo <= 4)
            {  
                NextLevel();
            ball.SceneStart();
            }
        else DestroyObjects(); 
        } 
        
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevelNo);
        
        currentLevelNo++;
    }
    public void Restart()
    {
        SceneManager.LoadScene(2);
        Destroy(gameObject);
    }
    public void DestroyObjects()
    {
        Destroy(Ball);
        Destroy(Platform);
    }      
}
