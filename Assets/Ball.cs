using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public int StartForce;
    // Start is called before the first frame update
    void Start()
    {

        rb.AddForce(new Vector2(0, StartForce));
    }

    // Update is called once per frame
    void Update()
    {

    }
    
        

}
