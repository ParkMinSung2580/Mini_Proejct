using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private IState<T> currentState;
    public IState<T> CurrentState => currentState;

    public void ChangeState(IState<T> nextState, T context)
    {
        currentState?.Exit(context);
        currentState = nextState;
        currentState.Enter(context);
    }

    public void UpdateState(T context)
    {
        currentState?.Update(context);
    }

    public string GetStateName()
    {
        return currentState == null ? "None" : currentState.GetType().Name;
    }
}
