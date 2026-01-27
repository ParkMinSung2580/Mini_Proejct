using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState CurrentState { get; private set; }
    public void ChangeState(IEnemyState next)
    {
        CurrentState?.Exit();
        CurrentState = next;
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public string GetStateString()
    {
        return CurrentState == null ? "None" : CurrentState.GetType().Name;
    }
}
