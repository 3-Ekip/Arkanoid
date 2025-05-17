using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth;
    public GameObject logicManagerr;
    LogicScript logic;
    public int BrickType;
    public GameObject damageCapsule;
    // Start is called before the first frame update
    void Start()
    {
        logicManagerr = GameObject.Find("LogicManager");
        logic = logicManagerr.GetComponent<LogicScript>();
        logic.bricksLeft++;
        if (brickHealth == 999)
        {
            logic.BrickKey++;
        }
    }

    void Update()
    {            
        if (brickHealth == 0) 
        {
            logic.bricksLeft--;
            Destroy(gameObject);
        }
        if (logic.bricksLeft == logic.BrickKey)
          {
            brickHealth = 1;
          }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "ball")
       {
            if(BrickType == 1)
            {
                Quaternion Rot = Quaternion.Euler(0, 0, Random.Range(-10, 10));
                Instantiate(damageCapsule, transform.position, Rot);
                
            }
            brickHealth--;
       }  
       
    }
    
}
