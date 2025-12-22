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
    public GameObject RespawnButton;
    public bool BallIsActive=false; //Ball is colliding with bricks and the platform therefore is not stuck
    public bool StartTimePeriod; //TheBoolThatChecksIfTheBallShouldBeLockedRightAboveThePlatformOrNot
    // Start is called before the first frame update
    void Start()
    {
        Default = GetComponent<SpriteRenderer>().color;
        DontDestroyOnLoad(this.gameObject);
        StartTimePeriod = true;
        SceneStart();
        SubscribeOnDestroy();
        StartCoroutine(SaveTheBallFromBeingStuck());
    }
    public void BallStartDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos, Platform.negativemaxX, Platform.maxX);
        transform.position = new Vector2(clampedX, -4.756f);
    }
    public void SceneStart()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(Pscript.transform.position.x, Pscript.transform.position.y+0.244f);
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
            StartCoroutine(BallHasInteracted());
            rb.velocity = new Vector2(0, 0);
            Vector2 normal = transform.position - new Vector3(Pscript.transform.position.x, Pscript.transform.position.y - (float)0.75, 0);
            rb.AddForce(normal.normalized * StartForce, ForceMode2D.Impulse);
        }
        if (collision.gameObject.tag == "floor")
        {
            StartCoroutine(BallHasInteracted());
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
        if (collision.gameObject.tag == "Brick")
        {
            Debug.Log("BallHasHitBrick");
            StartCoroutine(BallHasInteracted());
        }
    }
    IEnumerator BallHasInteracted()
    {
        if (BallIsActive) { yield break; }
        Debug.Log("BallHasHitBrickAndTheCRStarted");
        BallIsActive = true;
        if (RespawnButton== true) { RespawnButton.SetActive(false); }
        yield return new WaitForSeconds(1f);
        BallIsActive = false;
        Debug.Log("BallHasHitBrickAndTheCREnded");
    }
    public void RespawnFunction()
    { StartCoroutine(Respawn()); }
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
    IEnumerator SaveTheBallFromBeingStuck()
    {
        while (true)
        {
            int redo = 0;
            while (redo < 9)
            {
                yield return new WaitForSeconds(1f);
                if (BallIsActive) {break;}
                redo++;
                if (redo == 8) { RespawnButton.SetActive(true); }
            }                         
        }
    }
}
