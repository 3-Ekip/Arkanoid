using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (brickHealth == 0) 
        { 
            Destroy(gameObject);
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
