using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth;
    public GameObject logicManagerr;
    LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logicManagerr = GameObject.Find("LogicManager");
        logic = logicManagerr.GetComponent<LogicScript>();
        logic.bricksLeft++;

    }

    void Update()
    {
        if (brickHealth == 0) 
        {
            logic.bricksLeft--;
            Destroy(gameObject);
        }
        if (logic.BrickKey == true)
          {
            brickHealth = 1;
          }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "ball")
       {
            brickHealth--;
       }
    }
}
