using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInpuControl inputControl;//Unity自带的控制方法
    public Vector2 inputDirections;//创建二维的输入方向
    public float speed = 500f;//定义公开参数-速度
    private Rigidbody2D rb;//创建刚体组件```
    [Header("Basic")]
    private SpriteRenderer rbSprite;//创建渲染组件
    public float jumpForce;
    private PhysicsCheck physicsCheck;//定义physics参数

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInpuControl();//实例化inputContral
        inputControl.Gameplay.Jump.started += Jump;//注册跳跃动作，started为按1次触发
        //rbSprite = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();//获得变量
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirections = inputControl.Gameplay.Move.ReadValue<Vector2>();//跟Unity自带的输入控制面板里的组件是一致的

    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        rb.velocity = new Vector2(inputDirections.x * speed * Time.deltaTime, rb.velocity.y);
        //if(inputDirections.x<0)
        //rbSprite.flipX = true;
        //else rbSprite.flipX = false;  注意，这里不要用else，要用第二个if

        int faceDir = (int)transform.lossyScale.x;
        if (inputDirections.x > 0) { faceDir = 1; }
        if (inputDirections.x < 0) { faceDir = -1; }
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); 
            Debug.Log("Jump");
        }//在y轴正方向，即up上施加力
       
    }

}
