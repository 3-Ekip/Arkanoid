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
    public GameManager logic;
    public Platform Pscript; 
    public bool StartTimePeriod; //TheBoolThatChecksIfTheBallShouldBeLockedRightAboveThePlatformOrNot
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartTimePeriod = true;
        SceneStart();
    }
    public void BallStartDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos, Pscript.negativemaxX, Pscript.maxX);
        transform.position = new Vector2(clampedX, -4.76f);
    }
    public void SceneStart()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(Pscript.transform.position.x, Pscript.transform.position.y+0.24f);
        StartCoroutine(StartingPosition());
    }
    IEnumerator StartingPosition()
    {
        yield return new WaitForSeconds(1.5f);
        ShootUp();
    }
    void ShootUp()
    {
        Debug.Log("ShootUp");
        StartTimePeriod = false;
        Quaternion Rot = Quaternion.Euler(0, 0, Random.Range(-20, 20));
        transform.rotation = Rot;
        rb.AddRelativeForce(new Vector2(0, StartForce), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            rb.velocity = new Vector2(0, 0);
            Vector2 normal = transform.position - new Vector3(Pscript.transform.position.x, Pscript.transform.position.y - (float)1.25, 0);
            
            rb.AddForce(normal.normalized * StartForce, ForceMode2D.Impulse);
        }
        if (collision.gameObject.tag == "floor")
        {
            Debug.Log("Collided with floor");
            Pscript.HealthDecrease();
            StartCoroutine(Respawn());     
        }
    }
    public IEnumerator Respawn()
    {
        Debug.Log("Respawn");
        StartTimePeriod = true;
        rb.velocity = new Vector2(0, 0);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector2(Pscript.transform.position.x, Pscript.transform.position.y+(float)0.24);
        yield return new WaitForSeconds(1f);        
        gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        yield return new WaitForSeconds(1f);
        ShootUp();
    }
    
   
    
}
