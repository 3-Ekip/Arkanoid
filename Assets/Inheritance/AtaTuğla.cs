using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaTuðla : MonoBehaviour
{
    public void Yaz()
    {
        Çalýþ();
        Debug.Log("Ben Ata Sýnýfýyým");
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
}
