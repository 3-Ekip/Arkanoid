using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaTu�la : MonoBehaviour
{
    public void Yaz()
    {
        �al��();
        Debug.Log("Ben Ata S�n�f�y�m");
    }
    private void �al��()
    {
        Debug.Log("�al��");
    }
    protected void Dinlen()
    {
        Debug.Log("Dinlen");
    }   
    public virtual void OyunOyna()
    {
        Debug.Log("Oyun Oyna");
    }
    public virtual void K�r�l()
    {
        Debug.Log("Tu�la K�r�ld�");
    }
    public virtual void GetHit()
    {
        Debug.Log("K�r�l");
    }   
}
