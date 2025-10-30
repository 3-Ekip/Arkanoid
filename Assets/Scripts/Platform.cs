using System;
using System.Collections;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public Ball ball;
    public float speed = 5f;
    public static float maxX = 2.24f ;
    public static float negativemaxX = -2.24f;
    public GameManager logic;
    public GameObject Beam;
    public GameObject ShieldThatIsInstantiated;
    public GameObject shield;
    public GameObject PTurretR;
    public GameObject PTurretL;

    public bool BeamIsActive = false;
    public int TheShieldIsActive = 0;
    ShieldScript shieldscript;
    public event Action SyncTheShield;
    
    public event Action SyncThePTurret;    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SubscribeOnDestroy();
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
            if (ball.StartTimePeriod)
            {
                ball.BallStartDrag();
            }
            if(PTurretScript.PTurretActive>0)
            {
                (SyncThePTurret)?.Invoke();
            }
        }
    }
    
    public void platformDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos, negativemaxX, maxX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
        if (collision.gameObject.tag == "PowerUp")
        {
            float randomPwrUpGen= UnityEngine.Random.Range(1, 100);
            if (randomPwrUpGen <25)
            {
            InstantiateBeam();                
            }
            else if (randomPwrUpGen <50)
            {
                InstantiatePTurret();
            }
            else if (randomPwrUpGen <=75)
            {
                logic.HealthPoints+=3;
                logic.UpdateHealth();
            }
            else if (randomPwrUpGen <=100)
            {
                ball.ProtectedBallFunction();
            }
        }
    }
    public void InstantiatePTurret()
    {
        Instantiate(PTurretL, new Vector2(transform.position.x - 0.675f, transform.position.y + 0.24f), transform.rotation);
        Instantiate(PTurretR, new Vector2(transform.position.x + 0.675f, transform.position.y + 0.24f), transform.rotation);
    }   
    public void InstantiateShield()
    {
        Vector2 shieldpos = new Vector2(transform.position.x, transform.position.y + 0.6f);
        Instantiate(ShieldThatIsInstantiated, shieldpos, transform.rotation);
    }
    public void InstantiateBeam()
    {
        StartCoroutine(InstantiateBeamCR());
    }
    public IEnumerator InstantiateBeamCR()
    {
        Vector2 beampos = new Vector2(transform.position.x, 0);
        GameObject xyz = Instantiate(Beam, beampos, transform.rotation);
        yield return new WaitForSeconds(0.7f);
        Destroy(xyz);
    }
    public void HealthDecrease()
    {
        if (ball.isProtected)
        {
            ball.isProtected = false;
            ball.SetBackToDefault();
            return;
        }
        logic.HealthPoints--;
        if (logic.HealthPoints <= 0)
        {          
            logic.RestartGame();
        }
        logic.UpdateHealth();
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
}
