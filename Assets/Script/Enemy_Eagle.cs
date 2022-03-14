using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    //private Collider2D coll;
    public Transform top, bottom;
    public float Speed;
    private float TopY, BottomY;

    private bool isUP=true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        TopY = top.position.y;
        BottomY = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isUP)
        {
            rb.velocity = new Vector2(0, Speed);
            if(transform.position.y>TopY)
            {
                isUP = false;
            }    
        }
        else
        {
            rb.velocity = new Vector2(0, -Speed);
            if(transform.position.y<BottomY)
            {
                isUP = true;
            }
        }
    }
}
