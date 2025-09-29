using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth;
    public LogicScript logic;
    public int brickType; // 1=barikat, 2=kapsul, 3=taret, 0=normal
    public GameObject Capsule;
    public GameObject TurretPart;
    public GameObject barricade;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<LogicScript>();
        logic.bricksLeft++;
        if (brickType == 1)
        {
            logic.BrickKey++;
        }
        if (brickType == 3)
        {
            StartCoroutine(TurretShoot());
        }
        SubscribeToLogic();
    }
    IEnumerator TurretShoot() //brick tipi 3 ise
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Quaternion Turret = Quaternion.Euler(0, 0, Random.Range(-8, 8));
            TurretPart.transform.rotation = Turret;
            yield return new WaitForSeconds(0.2f);
            Instantiate(Capsule, transform.position, Turret);
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
        Destroy(gameObject);
    }
    void BrickDrop()
    {
        if (brickType == 2)
        {
            Quaternion Rot = Quaternion.Euler(0, 0, Random.Range(-10, 10));
            Instantiate(Capsule, transform.position, Rot);
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