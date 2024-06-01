using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{


    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    public override void Move()
    {

        

        if (physicsCheck.isGround && !isWait)
            rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);

        if (!isWait)
        {
            Debug.Log("walk");
            anim.SetBool("walk", true);
        }
        if (isWait)
        {
            Debug.Log("wait");
            anim.SetBool("walk", false);
        }

    }
}
