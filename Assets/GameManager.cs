using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int bricksLeft;
    public int GameOverSceneNum;
    public int BrickKey;
    public int HealthPoints;
    public int currentLevelNo;
    public int LastScene;
    public event Action RemoveBarricades;
    GameObject Platform;
    GameObject Ball;
    public GameObject Canvas;
    public Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        Platform = GameObject.Find("Platform");
        Ball = GameObject.Find("Ball");
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Canvas);
        currentLevelNo++;
    }

    // Update is called once per frame
    void Update()
    {
             
    }
    public void NextLevel()
    {
        
        ball.StartForce += (float)0.8;
        ball.SceneStart();
        ball.StartTimePeriod = true;
        BrickKey = 0;
        bricksLeft = 0;
        SceneManager.LoadScene(currentLevelNo);   
        currentLevelNo++;
        if (currentLevelNo == GameOverSceneNum)
        {
            DestroyObjects();
        }   
    }
    public void DestroyObjects()
    {
        Destroy(Ball);
        Destroy(Platform);
        Destroy(Canvas);
    }
    public void CheckBarricade() 
    {
        if (bricksLeft == BrickKey)
        {        
            (RemoveBarricades)?.Invoke();  
            RemoveBarricades = null;
        }          
    }  
    public void RestartGame()
    {
        SceneManager.LoadScene(GameOverSceneNum);
        currentLevelNo = GameOverSceneNum;
        DestroyObjects();
    }
}
