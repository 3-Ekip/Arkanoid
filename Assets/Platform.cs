using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    private int direction = 0;
    public float maxX;
    public float  negativemaxX;
    public LogicScript logic;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            platformDrag();
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
        if (collision.gameObject.tag == "damage")
        {
            logic.HealthPoints--;
        }
        if (collision.gameObject.tag == "heart")
        {
            logic.HealthPoints++;
        }
    }
}
