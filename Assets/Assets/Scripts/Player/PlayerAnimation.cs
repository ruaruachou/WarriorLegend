using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    PhysicsCheck physicsCheck;
    PlayerController playerController;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {

    }


    void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        SetVelocittyY();
        SetVelocityX();
        SetGroundedd();
        SetDead();
        SetAttack();
    }
    public void SetVelocityX()
    {
        anim.SetFloat("playerVelocityX", Mathf.Abs(rb.velocity.x));
    }
    public void SetVelocittyY()
    {
        anim.SetFloat("playerVelocityY", rb.velocity.y);
    }
    public void SetGroundedd()
    {
        anim.SetBool("isGround", physicsCheck.isGround);
    }

    public void SetHurt()
    {
        anim.SetTrigger("hurt");
    }

    public void  TriggerAttack(){
        anim.SetTrigger("attack");
        
    }
    public void SetDead()
    {
        anim.SetBool("isDead", playerController.isDead);
    }

    public void SetAttack()
    {
        anim.SetBool("isAttack", playerController.isAttack);
    }

}
