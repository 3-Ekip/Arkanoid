using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KumTuğla : AtaTuğla
{
    public override void OyunOyna()
    {
        Debug.Log("Oyun Oynama");
    }
    public override void Kırıl()
    {
        base.Kırıl();
        Debug.Log("Özellik düşür");
    }
    public override void GetHit()
    {
        Debug.Log("Can Azalt");
    }
    public override void SoyutMethod()
    {
        throw new System.NotImplementedException();
    }
}

