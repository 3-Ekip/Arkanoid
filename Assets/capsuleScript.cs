using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int capSpeed;
    public GameManager logic;
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<GameManager>();
        transform.position = new Vector2(transform.position.x, transform.position.y- (float)0.11);
        rb.AddRelativeForce(new Vector2(0, -capSpeed), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform") || this.gameObject.CompareTag("damage")&collision.gameObject.CompareTag("shield") || transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
}   
