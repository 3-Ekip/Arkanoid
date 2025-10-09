using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;//gereksiz
    public Ball ball;
    public float speed = 5f;
    public float maxX;
    public float negativemaxX;
    public GameManager logic;
    public GameObject ShieldThatIsInstantiated;
    public GameObject shield;
    public GameObject Beam;
    public bool BeamIsActive = false;
    public int TheShieldIsActive = 0;
    ShieldScript shieldscript;
    public event Action SyncTheShield;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            platformDrag();
            if (TheShieldIsActive >0)
            {
                shield = GameObject.Find("Shield(Clone)");
                shieldscript = shield.GetComponent<ShieldScript>();
                (SyncTheShield)?.Invoke();
                shieldscript.ShieldDrag();
                Debug.Log("Shield Dragged");
            }
            if (BeamIsActive)
            {
                GameObject beam = GameObject.Find("Beam(Clone)");
                beam.transform.position = new Vector2(transform.position.x, 0);
            }
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
            logic.UpdateHealth(); 
        }
        if (collision.gameObject.tag == "shieldcapsule")
        {
            InstantiateShield();
        }
    }
    public void InstantiateShield()
    {
        Vector2 shieldpos = new Vector2(transform.position.x, transform.position.y + 0.6f);
        Instantiate(ShieldThatIsInstantiated, shieldpos, transform.rotation);
    }
    public void InstantiateBeam()
    {
        Vector2 beampos = new Vector2(transform.position.x, 0);
        Instantiate(Beam, beampos, transform.rotation);
        GameObject beam = GameObject.Find("Beam(Clone)");
        DontDestroyOnLoad(beam);
        BeamIsActive = true;
    }
    public void HealthDecrease()
    {
        logic.HealthPoints--;
        if (logic.HealthPoints <= 0)
        {          
            logic.RestartGame();
        }
        logic.UpdateHealth();
    }
}
