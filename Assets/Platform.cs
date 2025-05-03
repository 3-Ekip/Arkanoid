using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    private int direction = 0;
    public float maxX = 1.355f;
    public float  negativemaxX = -1.355f;

    // Update is called once per frame
    void Update()
    {

        if (direction != 0)
        {
            transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

            
            float clampedX = Mathf.Clamp(transform.position.x, negativemaxX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
    void kod()
    {

    }
    public void ToggleDirection()
    {
        if (direction == 0)
        {
            direction = 1;  
        }
        else
        {
            direction *= -1;  
        }
    }


}
