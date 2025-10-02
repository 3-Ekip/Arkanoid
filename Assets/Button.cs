using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
    
{
    public GameManager logic;
    public GameObject GameManager;
    private void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<GameManager>();
        GameManager = GameObject.Find("LogicManager");
    }
    public void ButtonPress()
    {
        Destroy(GameManager);
        SceneManager.LoadScene(2);        
    }
}
