using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    abstract public void OnStateEnter();
    abstract public void OnStateLogicUpdate();
    abstract public void OnStatePhysicsUpdate();
    abstract public void OnStateExit();
}
