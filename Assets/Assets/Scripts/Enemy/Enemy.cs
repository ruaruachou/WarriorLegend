using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected Animator anim;
    public PhysicsCheck physicsCheck;

    [Header("基本参数 ")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public float hurtForce;
    public float hurtWaitTime;

    public Vector3 faceDir;

    [Header("状态")]

    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    public bool isWait;
    public float waitTime;
    public float waitCounter;
    public bool isPatrol;
    public bool isChase;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
    }
    void OnEnable()
    {
        currentState = patrolState;
        //currentState.OnStateEnter(this);
    }
    void Start()
    {
        waitCounter = waitTime;
    }
    void Update()
    {

        GetFaceDir();
        SetFaceDir();
        WaitCounter();

        currentState.OnStateLogicUpdate();

    }
    void FixedUpdate()
    {
        Move();

        currentState.OnStatePhysicsUpdate();

    }

    private void OnDisable()
    {
        currentState.OnStateExit();
    }

    public void GetFaceDir()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }
    public void SetFaceDir()
    {

        if ((faceDir.x < 0 && physicsCheck.touchLeftWall) || (faceDir.x > 0 && physicsCheck.touchRightWall))
        {
            isWait = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public virtual void Move()
    {
        
    }

    public void WaitCounter()
    {
        if (isWait)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter < 0)
            {
                isWait = false;
                waitCounter = waitTime;
            }
        }
    }

    public void GetHurt(Transform attackerTransform)
    {
        StartCoroutine(WaitFromHurt(attackerTransform));
        anim.SetTrigger("pigHurt");
    }

    public void GetDead()
    {
        anim.SetBool("pigDead", true);
        //关闭所有物理组件
        rb.simulated = false;
        Destroy(gameObject, 1f);
    }

    IEnumerator WaitFromHurt(Transform Atktransform)
    {
        isWait = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        Vector2 dir = new Vector2(transform.position.x - Atktransform.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(hurtWaitTime);
        isWait = false;
    }
}
