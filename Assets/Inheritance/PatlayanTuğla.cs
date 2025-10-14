using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatlayanTuğla : AtaTuğla
{
    public override void OyunOyna()
    {
        Debug.Log("Oyun Oynama");
    }
    public override void GetHit()
    {
        Debug.Log("Patla");
    }
}
