using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHp;
    public float currentHp;

    [Header("Invincible")]
    public float invincibleDurTime;
    public float currentInvincibleTime;
    public bool isInvincible;

    public UnityEvent<Transform> onTakeDamage;

    void Awake()
    {
        currentHp = maxHp;
        
    }

    void Start()
    {
        isInvincible = false;
        currentHp = maxHp;
    }

    void Update()
    {
        CheckInvicible();
    }

    public UnityEvent onDie;

    public void TakeDamage(Attack atttacker)
    {
        if (isInvincible) return;

        if (currentHp - atttacker.damage > 0)
        {
            currentHp -= atttacker.damage;
            SetInvincible();
            //打印，obj受到来自XXX的XX伤害
            Debug.Log($"{gameObject.name} took {atttacker.damage} damage from {atttacker.gameObject.name}");

            onTakeDamage?.Invoke(atttacker.transform);
        }

        else
        {
            currentHp = 0;
            onDie?.Invoke();
        }
    }



    public void CheckInvicible()
    {
        if (isInvincible)
        {
            currentInvincibleTime -= Time.deltaTime;
            if (currentInvincibleTime <= 0)
            {
                isInvincible = false;
            }
        }
    }
    public void SetInvincible()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            currentInvincibleTime = invincibleDurTime;
        }
    }

}
