using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    public IState CurrentState => currentState;

    public void ChangeState(IState nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public string GetStateName()
    {
        return currentState == null ? "None" : currentState.GetType().Name;
    }
}
