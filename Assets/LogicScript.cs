using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LogicScript : MonoBehaviour
{
    public int bricksLeft;
    public bool BrickKey;
    public int HealthPoints;
    public int currentLevelNo;
    GameObject Ball;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.Find("Ball");
        ball = Ball.GetComponent<Ball>();
        DontDestroyOnLoad(this.gameObject);
        currentLevelNo++;
    }

    // Update is called once per frame
    void Update()
    {       
        if (bricksLeft == 1)
        {
            BrickKey = true;
        }
        if (bricksLeft == 0) 
        {
            if (currentLevelNo <= 4) 
            NextLevel();
        } 
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevelNo);
        ball.SceneStart();
        currentLevelNo++;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
