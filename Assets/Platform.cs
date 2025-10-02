using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;//gereksiz
    public Ball ball;
    public float speed = 5f;
    public float maxX;
    public float  negativemaxX;
    public GameManager logic;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            platformDrag();
            if (ball.StartTimePeriod)
            {
                ball.BallStartDrag();
            }
        }
    }
    public void platformDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos, negativemaxX, maxX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "damage")//switch-case olabilir
        {
            HealthDecrease();
        }
        if (collision.gameObject.tag == "heart")
        {
            logic.HealthPoints++;
        }
    }
    public void HealthDecrease()
    {
        logic.HealthPoints--;
        if (logic.HealthPoints <= 0)
        {          
            logic.RestartGame();
        }
    }
}
