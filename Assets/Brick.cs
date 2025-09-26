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
    public int brickType;
    public GameObject Capsule;
    public GameObject TurretPart;
    public GameObject barricade;
    // Start is called before the first frame update
    void Start()
    {
        logicManagerr = GameObject.Find("LogicManager");
        logic = logicManagerr.GetComponent<LogicScript>();
        logic.bricksLeft++;
        if (brickType == 1)
        {
            logic.BrickKey++;
        }
        StartCoroutine(TurretShoot());
    }

    void Update()
    {            
        
        
    }
    

    IEnumerator TurretShoot()
    {
        while (brickType == 3)
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
            BrickHit();
            BrickDrop();

       }  
       
    }
    void BrickHit()
    {
        if (brickHealth == 0)
        {
            if (brickType == 1)
            {
                logic.BrickKey--;
            }
            logic.bricksLeft--;
            Destroy(gameObject);
        }
        if (logic.bricksLeft == logic.BrickKey)
        {
            Destroy(barricade);
            brickHealth = 1;
        }
    }
    void BrickDrop()
    {

        if (brickType == 2)
        {
            Quaternion Rot = Quaternion.Euler(0, 0, Random.Range(-10, 10));
            Instantiate(Capsule, transform.position, Rot);
        }
        if (brickType == 4)
        {
            Quaternion Rot = Quaternion.Euler(0, 0, Random.Range(-10, 10));
            Instantiate(Capsule, transform.position, Rot);
            Debug.Log("instantiated");
        }
        brickHealth--;
    }
    
}
