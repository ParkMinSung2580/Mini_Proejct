using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyStateBase
{
    public ChaseState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm) { }

    public override void Enter()
    { 
        Debug.Log("Chase 시작");
    }
    public override void Update()
    {
        if (!enemy.CanSeePlayer())
        {
            fsm.ChangeState(new IdleState(enemy, fsm));
        }
    }
    public override void Exit() { Debug.Log("Chase 종료"); }
}
