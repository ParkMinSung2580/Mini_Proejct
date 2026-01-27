using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//제약 조건을 걸어서 FSM이 있는 T 객체만 BaseState를 만들 수 있다.
public abstract class BaseState<T> : IState<T> where T : IStateContext<T>
{
    public virtual void Enter(T context) { Debug.Log($"{context.FSM.GetStateName()} Enter"); }
    public virtual void Update(T context) { }
    public virtual void Exit(T context) { Debug.Log($"{context.FSM.GetStateName()} Exit"); }
}
