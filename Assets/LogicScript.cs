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
    public Array Scenes;
    public int currentLevelNo;
    // Start is called before the first frame update
    void Start()
    {
        currentLevelNo = 0;
    }

    // Update is called once per frame
    void Update()
    {       
        if (bricksLeft == 1)
        {
            BrickKey = true;
        }        
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevelNo);
        currentLevelNo++;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
