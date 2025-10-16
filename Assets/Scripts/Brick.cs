using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject Explosion;
    public int brickHealth;
    public GameManager logic;
    public int brickType; // 1=barikat, 2=kapsul, 3=taret, 0=normal
    public GameObject Capsule;
    public GameObject TurretPart;
    public GameObject barricade;
    public Ball ball;
    public LayerMask targetLayers; 
    public GameObject PowerUp;
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
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            BrickDrop();
            BrickHit();
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TriggerStay");
        if (collision.gameObject.tag == "ball")
        {
            BrickDrop();
            BrickHit();
        }
        Debug.Log("TriggerStayEnd");
    }

    public virtual void BrickHit()
    {
        brickHealth--;

        if (brickHealth == 0) 
        {
            BrickDie();
        }
    }
    public void BrickDie()
    {
        if (brickType == 1) //barikat kitliyse
        {
            logic.BrickKey--; //toplam
        }
        if (brickType == 4) //patlayýcý tuðla ise
        {
            StartCoroutine(VoidThatExplodes());
        }
        else
        {
            BrickDeathCheck();
        }
    }
    public void BrickDeathCheck()
    {
        logic.bricksLeft--;
        logic.CheckBarricade();
        if (logic.bricksLeft == 0)
        {
            if (logic.currentLevelNo <= logic.LastScene)
            {
                logic.VoidThatCallsNextLevel();
            }
            else
            {
                logic.DestroyObjects();
            }
            Destroy(gameObject);
        }
        float dropRNG = Random.Range(0f, 1f);
        if (dropRNG < 0.05f)
        {
            Instantiate(PowerUp, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    
    public IEnumerator VoidThatExplodes()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.6f);
        Vector2 center = transform.position;
        Vector2 boxSize = new Vector2(1.5f, 1.25f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, boxSize, 0f, targetLayers);
        foreach (Collider2D col in hits)
        {      
               Brick BlownUpBrick = col.GetComponent<Brick>();
               if (BlownUpBrick?.brickType==2)
               {
                   BlownUpBrick?.BrickDrop();
               }
               BlownUpBrick?.BrickHit();          
        }
        BrickDeathCheck();
    }


    public void BrickDrop()
    {
        if (brickType == 2)
        {
            BrickDropCapsule();
        }       
    }
    public void BrickDropCapsule()
    {
        Instantiate(Capsule, transform.position, transform.rotation);
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