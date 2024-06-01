using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigState : BaseState
{
    public override void OnStateEnter(Enemy enemy)
    {
        currenyEnemy = enemy;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateLogicUpdate()
    {
        Debug.Log("PigState Logic Update");
    }

    public override void OnStatePhysicsUpdate()
    {
        
    }
}


