using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PTurretScript : MonoBehaviour
{
    
    public int TotalBullets;
    public GameObject Capsule;
    public static int PTurretActive=0;
    public Platform platform;
    public float RorL; // Right or Left
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        platform = GameObject.Find("Platform").GetComponent<Platform>();
        SubscribeToPlatform();
        PTurretActive++;
        StartCoroutine(PTurretShoot());
    }
    public void PTurretDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos+RorL, Platform.negativemaxX+RorL, Platform.maxX+RorL);
        transform.position = new Vector2(clampedX, transform.position.y);
        
    }
    void SubscribeToPlatform() 
    { 
        platform.SyncThePTurret += PTurretDrag;
    }
    IEnumerator PTurretShoot() 
    {
        while (TotalBullets>0)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(Capsule, transform.position, transform.rotation);
            TotalBullets--;
        }
        platform.SyncThePTurret -= PTurretDrag;
        PTurretActive--;
        Destroy(this.gameObject);
    }
}
