using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int capSpeed;
    public float Ycapsuleheightdiff;
    public GameManager logic;
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<GameManager>();
        transform.position = new Vector2(transform.position.x, transform.position.y- Ycapsuleheightdiff);
        rb.AddRelativeForce(new Vector2(0, -capSpeed), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Capsule collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("platform")||
            collision.gameObject.CompareTag("floor") || 
            collision.gameObject.CompareTag("WALL") || 
            this.gameObject.CompareTag("ball") && collision.gameObject.CompareTag("Brick") ||
            this.gameObject.CompareTag("damage")&collision.gameObject.CompareTag("shield") ||
            transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
}   
