using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
    
{
    public LogicScript logic;
    public void ButtonPress()
    {
        logic.currentLevelNo++;
        SceneManager.LoadScene(2);        
    }
}
