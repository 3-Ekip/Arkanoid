using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int StartForce;
    public float yyy;
    // Start is called before the first frame update
    void Start()
    {

        rb.AddForce(new Vector2(0, StartForce));
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, -StartForce));    
        }
    }



}
