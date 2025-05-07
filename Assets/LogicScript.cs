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
    // Start is called before the first frame update
    void Start()
    {
        
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
        //SceneManager.LoadScene(1);
    }
}
