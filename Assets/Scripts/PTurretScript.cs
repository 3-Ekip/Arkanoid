using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PTurretScript : MonoBehaviour
{
    public int TotalBullets = 5;
    public GameObject Capsule;
    public static bool PTurretActive;
    public Platform platform;
    public float RorL; // Right or Left
    void Start()
    {
        platform = GameObject.Find("Platform").GetComponent<Platform>();
        SubscribeToPlatform();
        PTurretActive = true;
        StartCoroutine(PTurretShoot());
    }
    public void PTurretDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos+RorL, Platform.negativemaxX, Platform.maxX);
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
            yield return new WaitForSeconds(0.5f);
            Instantiate(Capsule, transform.position, transform.rotation);
            TotalBullets--;
        }
        PTurretActive = false;
        platform.SyncThePTurret -= PTurretDrag;
        Destroy(this.gameObject);
    }
}
