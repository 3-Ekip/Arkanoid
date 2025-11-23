using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class Platform : MonoBehaviour
{
    public Ball ball;
    public float speed = 5f;
    public static float maxX = 2f;
    public static float negativemaxX = -2f;
    public GameManager logic;
    public GameObject BeamBase;
    public GameObject ShieldThatIsInstantiated;
    public GameObject shield;
    public GameObject PTurretR;
    public GameObject PTurretL;
    public GameObject BeamInScene;
    public bool BeamIsActive = false;
    public int TheShieldIsActive = 0;
    ShieldScript shieldscript;
    public event Action SyncTheShield;
    public bool IsBeamOn;
    public event Action SyncThePTurret;
    public int randomPwrUpGen;
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
            }
            if (ball.StartTimePeriod)
            {
                ball.BallStartDrag();
            }
            if(PTurretScript.PTurretActive>0)
            {
                (SyncThePTurret)?.Invoke();
            }
            if (IsBeamOn)
            {
                BeamInScene.transform.position = this.transform.position;
                Debug.Log("BeamDragged");
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
            randomPwrUpGen= UnityEngine.Random.Range(1, 100);
            if (randomPwrUpGen <=30)
            {
                PowerUpBeamPart();
            }
            else
            {
                PowerUpNonBeamPart();
            }
            
        }
    }
    public void PowerUpBeamPart()
    {     
            if (IsBeamOn == false)
            {
                InstantiateBeam();
            }
            else
            {
                randomPwrUpGen = UnityEngine.Random.Range(31, 100);
                PowerUpNonBeamPart();
            }     
    }
    public void PowerUpNonBeamPart()
    {
        if (randomPwrUpGen <= 60)
        {
            InstantiatePTurret();
        }
        else if (randomPwrUpGen <= 70)
        {
            if (ball.isProtected == true)
            {
                logic.HealthPoints += 1;
                logic.UpdateHealth();
                return;
            }
            ball.ProtectedBallFunction();
        }
        else
        {
            StartCoroutine(ThreeBrickDestroyerPowerUp());
        }
    }
    IEnumerator ThreeBrickDestroyerPowerUp()
    {
        GameObject bricks = GameObject.Find("Bricks");
        int ChildCount;
        int repeat = 0;
        while (repeat < 5)
        {
            ChildCount = bricks.transform.childCount;
            int RandomBrickIndex = UnityEngine.Random.Range(0, ChildCount);
            if (ChildCount == 0) { repeat++; yield break; }
            Transform randomChild = bricks.transform.GetChild(RandomBrickIndex);
            randomChild.GetComponent<Brick>().BrickDie();
            repeat++;
            yield return new WaitForSeconds(0.15f);
        }
    }
    public void InstantiatePTurret()
    {
        Instantiate(PTurretL, new Vector2(transform.position.x - 0.84f, transform.position.y + 0.3f), transform.rotation);
        Instantiate(PTurretR, new Vector2(transform.position.x + 0.84f, transform.position.y + 0.3f), transform.rotation);
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
        BeamInScene = Instantiate(BeamBase, transform.position, transform.rotation);
        IsBeamOn = true;
        yield return new WaitForSeconds(1f);
        IsBeamOn = false;
        Transform BeamT = BeamInScene.transform.Find("Beam");
        Transform SemiCircle = BeamInScene.transform.Find("SemiCircle");
        SemiCircle.gameObject.SetActive(false);
        BeamT.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Destroy(BeamInScene);
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
