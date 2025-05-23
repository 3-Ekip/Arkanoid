using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float StartForce;
    public GameObject ball;
    public LogicScript logic;
    public Transform pl;
    public Quaternion Rot;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneStart();
    }
    public void SceneStart()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        StartCoroutine(StartingPosition());
    }
    IEnumerator StartingPosition()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartFandR());
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
            }
            else
            {
                StartCoroutine(Respawn());
                Debug.Log("b");
            }
        }
    }
    IEnumerator Respawn()
    {
        rb.velocity = new Vector2(0, 0);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);        
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartFandR());
    }
    
    IEnumerator StartFandR()
    {
        Rot = Quaternion.Euler(0, 0, Random.Range(-20, 20));        
        transform.rotation = Rot;
        yield return new WaitForSeconds(0.1f);
        rb.AddRelativeForce(new Vector2(0, -StartForce), ForceMode2D.Impulse);
    }
}
