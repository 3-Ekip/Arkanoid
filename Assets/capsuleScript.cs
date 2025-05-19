using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int capSpeed;
    void Start()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y- (float)0.11);
        rb.AddRelativeForce(new Vector2(0, -capSpeed), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            Destroy(gameObject);
        }
    }
}   
