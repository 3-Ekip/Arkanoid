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
    public Platform Pscript;
    public Transform pl;
    public Quaternion Rot;
    public bool StartingTimePeriod;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartingTimePeriod = true;
        SceneStart();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (StartingTimePeriod == true)
            {
                BallStartDrag();
            }
            
        }

    }
    public void BallStartDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos, Pscript.negativemaxX, Pscript.maxX);
        transform.position = new Vector2(clampedX, (float)-4.76);
    }
    public void SceneStart()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(pl.transform.position.x, pl.transform.position.y+(float)0.24);
        StartCoroutine(StartingPosition());
    }
    IEnumerator StartingPosition()
    {
        yield return new WaitForSeconds(1.5f);
        Rotatshup();
    }
    void Rotatshup()
    {
        StartingTimePeriod = false;
        Rot = Quaternion.Euler(0, 0, Random.Range(-20, 20));
        transform.rotation = Rot;

        rb.AddRelativeForce(new Vector2(0, StartForce), ForceMode2D.Impulse);
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
        StartingTimePeriod = true;
        rb.velocity = new Vector2(0, 0);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector2(pl.transform.position.x, pl.transform.position.y+(float)0.24);
        yield return new WaitForSeconds(1f);        
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        yield return new WaitForSeconds(1f);
        Rotatshup();
    }
    
   
    
}
