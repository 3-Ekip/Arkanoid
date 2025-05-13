using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int StartForce;
    public float yyy;
    public GameObject ball;
    public LogicScript logic;
    public Transform pl;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 2);
        StartCoroutine("StartingForceCR");
    }
    IEnumerator StartingForceCR()
    {
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(new Vector2(0, -StartForce),ForceMode2D.Impulse);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            rb.velocity = new Vector2(0, 0);
            Vector2 normal = transform.position - new Vector3(pl.position.x, pl.position.y - (float)1.25, 0);
            
            rb.AddForce(normal.normalized * StartForce, ForceMode2D.Impulse);
        }
        if (collision.gameObject.tag == "floor")
        {
            logic.HealthPoints--;
            if (logic.HealthPoints == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                StartCoroutine("Respawn");
            }
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
        rb.AddForce(new Vector2(0, -StartForce), ForceMode2D.Impulse); 
    }
}
