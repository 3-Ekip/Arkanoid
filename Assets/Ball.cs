using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int StartForce;
    public float yyy;
    public GameObject ball;
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
        if (collision.gameObject.tag == "brick")
        {
            transform.position = new Vector2(0, 0);
        }
    }
    IEnumerator Respawn()
    {
        ball.SetActive(false);
        Debug.Log("ballsetfalse");  
        yield return new WaitForSeconds(0.5f);
        Debug.Log("firstwaitend");
        ball.SetActive(true);
        Debug.Log("ballsettrue");
        transform.position = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, -StartForce));
        Debug.Log("endofcr");
    }
}
