using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyStateBase
{
    public PatrolState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm) { }

    public override void Enter()
    {
        Debug.Log("Patrol 시작");
        enemy.animator.SetBool("IsMove", true);
    }
    public override void Update()
    {
        if (enemy.CanSeePlayer())
        {
            fsm.ChangeState(new ChaseState(enemy, fsm));
        }
    }
    public override void Exit() 
    {
        Debug.Log("Patrol 종료");
    }
}
