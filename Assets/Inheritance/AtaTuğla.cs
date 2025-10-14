using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtaTuðla : MonoBehaviour
{
    public static AtaTuðla instance;
    protected int sayý; 
    public void Yaz()
    {
        Çalýþ();
        Debug.Log("Ben Ata Sýnýfýyým");
        GameManager.instance.bricksLeft++;
    }
    private void Çalýþ()
    {
        Debug.Log("Çalýþ");
    }
    protected void Dinlen()
    {
        Debug.Log("Dinlen");
    }   
    public virtual void OyunOyna()
    {
        Debug.Log("Oyun Oyna");
    }
    public virtual void Kýrýl()
    {
        Debug.Log("Tuðla Kýrýldý");
    }
    public virtual void GetHit()
    {
        Debug.Log("Kýrýl");
    }
    public abstract void SoyutMethod();
}
