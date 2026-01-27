using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase : IState
{
    protected Enemy enemy;
    protected StateMachine fsm;
    protected EnemyStateBase(Enemy enemy, StateMachine fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public virtual void Enter() { Debug.Log($"{enemy.FSM.GetStateName()} Enter"); }
    public virtual void Update( ) { }
    public virtual void Exit() { Debug.Log($"{enemy.FSM.GetStateName()} Exit"); }
}
