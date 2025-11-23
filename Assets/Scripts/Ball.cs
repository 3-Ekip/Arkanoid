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
    public Sprite ProtectedBall;
    public Color Default;
    public Color ProtectedBallColor;
    public bool isProtected = false;
    public bool StartTimePeriod; //TheBoolThatChecksIfTheBallShouldBeLockedRightAboveThePlatformOrNot
    // Start is called before the first frame update
    void Start()
    {
        Default = GetComponent<SpriteRenderer>().color;
        DontDestroyOnLoad(this.gameObject);
        StartTimePeriod = true;
        SceneStart();
        SubscribeOnDestroy();
    }
    public void BallStartDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos, Platform.negativemaxX, Platform.maxX);
        transform.position = new Vector2(clampedX, -4.7f);
    }
    public void SceneStart()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(Pscript.transform.position.x, Pscript.transform.position.y+0.35f);
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
        if(rb.velocity.magnitude==0)
        {
            rb.AddRelativeForce(new Vector2(0, StartForce), ForceMode2D.Impulse);
        }

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
            if (isProtected)
            {
                isProtected = false;
                SetBackToDefault();
                StartCoroutine(Respawn());
                return;
            }
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
        transform.position = new Vector2(Pscript.transform.position.x, Pscript.transform.position.y+(float)0.3);
        yield return new WaitForSeconds(1f);        
        gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        yield return new WaitForSeconds(1f);
        ShootUp();
    }
    private void Destroy()
    {
        Destroy(gameObject);
        GameManager.Destruction -= Destroy;
    }
    void SubscribeOnDestroy()
    {
        GameManager.Destruction += Destroy;
    }
    public void ProtectedBallFunction()
    {
        isProtected = true;
        //gameObject.GetComponent<SpriteRenderer>().sprite = ProtectedBall;
        gameObject.GetComponent<SpriteRenderer>().color = ProtectedBallColor;
    }
    public void SetBackToDefault()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Default;
    }

}
