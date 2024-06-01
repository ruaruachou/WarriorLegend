using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInpuControl inputControl;
    public Vector2 inputDirections;
    public float speed = 500f;
    private Rigidbody2D rb;

    [Header("Basic")]
    private SpriteRenderer rbSprite;
    public float jumpForce;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    public PhysicsMaterial2D normalPhysicsMaterial;
    public PhysicsMaterial2D fraPhysicsMaterial;

    [Header("状态")]
    public bool isHurt;
    public float hurtForce;
    public bool isDead;
    public bool isAttack;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInpuControl();
        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += Attack;

        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Start()
    {

    }
    private void Update()
    {
        inputDirections = inputControl.Gameplay.Move.ReadValue<Vector2>();
        SetPhysicsMaterial();

    }
    private void FixedUpdate()
    {
        if (!isHurt)
            Move();
    }
    public void Move()
    {
        rb.velocity = new Vector2(inputDirections.x * speed * Time.deltaTime, rb.velocity.y);
        //if(inputDirections.x<0)
        //rbSprite.flipX = true;
        //else rbSprite.flipX = false; 
        int faceDir = (int)transform.localScale.x;
        if (inputDirections.x > 0) { faceDir = 1; }
        if (inputDirections.x < 0) { faceDir = -1; }
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void Attack(InputAction.CallbackContext obj)
    {
        playerAnimation.TriggerAttack();
        isAttack = true;
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;

        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        Debug.Log("反弹");
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void Die()
    {
        isDead = true;
        inputControl.Disable();
    }

    public void SetPhysicsMaterial()
    {
        if (physicsCheck.isGround && isAttack)
        {
            GetComponent<CapsuleCollider2D>().sharedMaterial = fraPhysicsMaterial;
        }
        else
        {
            GetComponent<CapsuleCollider2D>().sharedMaterial = normalPhysicsMaterial;

        }
    }

}
