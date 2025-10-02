using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth;
    public GameManager logic;
    public int brickType; // 1=barikat, 2=kapsul, 3=taret, 0=normal
    public GameObject Capsule;
    public GameObject TurretPart;
    public GameObject barricade;
    public Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<GameManager>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        logic.bricksLeft++;
        if (brickType == 1)
        {
            logic.BrickKey++;
        }
        if (brickType == 3)
        {
            StartCoroutine(TurretShoot());
            logic.BrickKey++;     
        }
        SubscribeToLogic();
    }
    IEnumerator TurretShoot() //brick tipi 3 ise
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Instantiate(Capsule, transform.position, transform.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            BrickDrop();
            BrickHit();
        }

    }
    void BrickHit()
    {

        brickHealth--;

        if (brickHealth == 0)
        {
            BrickDie();
        }
    }
    void BrickDie()
    {
        if (brickType == 1) //barikat kitliyse
        {
            logic.BrickKey--; //toplam
        }

        logic.bricksLeft--;
        logic.CheckBarricade();
        if (logic.bricksLeft == 0)
        {
            if (logic.currentLevelNo <= logic.LastScene)
            {
                logic.NextLevel();
            }
            else
            {
               logic.DestroyObjects();
                Destroy(gameObject);
            }
        }      
        Destroy(gameObject);
    }
    void BrickDrop()
    {
        if (brickType == 2)
        {
            Instantiate(Capsule, transform.position, transform.rotation);
        }       
    }
    void SubscribeToLogic()
    {
        logic.RemoveBarricades += RemoveBarricade;
    }
    void RemoveBarricade()
    {
        Destroy(barricade);
        brickHealth = 1; 
    }
}