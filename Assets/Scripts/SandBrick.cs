using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBrick : Brick
{
    public Sprite[] hitSprites= new Sprite[2];
    private void Start()
    {
        
    }
    override public void BrickHit()
    {
        brickHealth--;
        if (brickHealth == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[0];
        }
        else if (brickHealth == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[1];
        }
        else if (brickHealth <= 0)
        {
            BrickDie();
        }    
    }
}
