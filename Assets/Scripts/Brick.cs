using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

 public class Brick : MonoBehaviour
{
    public static int Max = 100;
    public GameObject Explosion;
    public int brickHealth;
    public GameManager logic;
    public int brickType; 
    public GameObject Capsule;
    public GameObject TurretPart;
    public GameObject barricade;
    public Ball ball;
    public LayerMask targetLayers; 
    public GameObject PowerUp;
    bool BrickIsDead = false;
    // Start is called before the first frame updpublicate
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
    IEnumerator TurretShoot() 
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
        if (BrickIsDead)
        {
            return;
        }
        BrickIsDead = true;
        if (brickType == 1) 
        {
            logic.BrickKey--; 
        }
        if (brickType == 4) 
        {
            VoidThatExplodes();
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
            return;
        }
        
        int dropRNG = Random.Range(0, Max);
        if (dropRNG < 10)
        {
            InstantiatePowerUp();
            Max = 100;
        }
        else
        {
            Max -= 1;
        }
        Debug.Log(Max);
        Destroy(gameObject);
    }
    public void InstantiatePowerUp()
    {
        Instantiate(PowerUp, transform.position, transform.rotation);
    }

    public void VoidThatExplodes()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;    
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
        GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(explosion, 0.8f);
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