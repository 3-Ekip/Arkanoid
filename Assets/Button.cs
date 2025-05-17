using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject logicManager;
    LogicScript lojic;
    void Start()
    {
        logicManager = GameObject.Find("LogicManager");
        lojic = logicManager.GetComponent<LogicScript>();
    }
    public void ButtonPress()
    {
        lojic.Restart();
    }
}
