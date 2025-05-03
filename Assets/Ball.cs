using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int StartForce;
    public float yyy;
    public GameObject ball;
    public Brick brickScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartingForceCR");
    }
    IEnumerator StartingForceCR()
    {
        yield return new WaitForSeconds(2f);
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
        if (collision.gameObject.tag == "floor")
        {
            StartCoroutine("Respawn");            
        }
        
    }
    IEnumerator Respawn()
    {
        rb.velocity = new Vector2(0, 0);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);        
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.position = new Vector2(0, 2);
        yield return new WaitForSeconds(1f);     
        rb.AddForce(new Vector2(0, StartForce)); 
    }
}
