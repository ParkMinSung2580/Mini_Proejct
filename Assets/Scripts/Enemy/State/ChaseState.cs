using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyStateBase
{
    public ChaseState(Enemy enemy, StateMachine fsm) : base(enemy, fsm) { }

    public override void Enter()
    { 
        enemy.SavePreChasePosition();
    }
    public override void Update()
    {
        if (!enemy.CanSeePlayer())
        {
            fsm.ChangeState(new IdleState(enemy, fsm));
        }
    }
    public override void Exit() { 
    }
}
