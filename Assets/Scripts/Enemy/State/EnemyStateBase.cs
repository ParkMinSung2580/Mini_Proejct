using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase : IEnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine fsm;
    protected EnemyStateBase(Enemy enemy, EnemyStateMachine fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update( ) { }
    public virtual void Exit() { }
}
