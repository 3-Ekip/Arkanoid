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
    public int LastScene;
    public event Action RemoveBarricades;
    GameObject Platform;
    public Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        Platform = GameObject.Find("Platform");
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
        
        if (bricksLeft == 0)
        {
            if (currentLevelNo <= LastScene)
            {  
                ball.SceneStart();
                ball.StartTimePeriod = true;
                NextLevel();
            }
            else
            {
                DestroyObjects();
                Destroy(gameObject);
            }            
        }        
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevelNo);
        ball.StartForce += (float)0.8;
        currentLevelNo++;
    }
    public void DestroyObjects()
    {
        Destroy(ball);
        Destroy(Platform);
    }
    public void CheckBarricade() 
    {
        if (bricksLeft == BrickKey)
        {        
            (RemoveBarricades)?.Invoke();  
            RemoveBarricades = null;
        }          
    }  
}
