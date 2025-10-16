using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public Platform platform;
    public float ShieldsY;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        platform = GameObject.Find("Platform").GetComponent<Platform>();
        SubscribeToSync();
        if (platform.TheShieldIsActive >0)
        {
            ShieldsY = ShieldsY + 0.2f*platform.TheShieldIsActive;
        }
        platform.TheShieldIsActive++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "damage")
        {           
            platform.SyncTheShield -= ShieldDrag;
            platform.TheShieldIsActive--;
            Destroy(this.gameObject);
        }
    }
    public void ShieldDrag()
    {
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float clampedX = Mathf.Clamp(mousePos,Platform.negativemaxX, Platform.maxX);
        transform.position = new Vector2(clampedX, ShieldsY);
    }
    void SubscribeToSync()
    {
       platform.SyncTheShield += ShieldDrag;
    }
}
