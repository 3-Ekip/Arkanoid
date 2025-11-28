    using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int bricksLeft;
    public int GameOverSceneNum;
    public int BrickKey;
    public int HealthPoints;
    public int currentLevelNo;
    public int LastScene;
    public event Action RemoveBarricades;
    public static event Action Destruction;
    public float BallSpeedAcc;
    GameObject Platform;
    GameObject Ball;
    public Platform platform;
    public GameObject Canvas;
    public Ball ball;
    public Text HealthText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }



        UpdateHealth();
        HealthText.text = "Health: " + HealthPoints.ToString();
        Platform = GameObject.Find("Platform");
        Ball = GameObject.Find("Ball");
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Canvas);
        currentLevelNo++;
    }
    public void UpdateHealth()
    {
        HealthText.text = "Health: " + HealthPoints.ToString();
    }

    public void VoidThatCallsNextLevel()
    {
        StartCoroutine(NextLevel());
    }
    public IEnumerator NextLevel()
    {
        Debug.Log("Next Level");
        yield return new WaitForSeconds(2f);
        ball.StartForce += BallSpeedAcc;
        ball.SceneStart();
        ball.StartTimePeriod = true;
        platform.OnlyOneBeamAtATime = false;
        platform.IsBeamOn = false;
        BrickKey = 0;
        bricksLeft = 0;
        SceneManager.LoadScene(currentLevelNo);   
        currentLevelNo++;
        if (currentLevelNo == GameOverSceneNum)
        {
            RestartGame();
        }
        if (currentLevelNo == LastScene)
        {
            DestroyObjects();
        }
        Debug.Log("Level Loaded");
    }
    public void DestroyObjects()
    {
        Destroy(Canvas);
        Destruction?.Invoke();
        Debug.Log("Destroyed Objects");
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
