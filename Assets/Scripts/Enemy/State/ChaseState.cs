using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState<Enemy>
{


    public override void Enter(Enemy enemy)
    {
        base.Enter(enemy);
        enemy.SavePreChasePosition();
    }
    public override void Update(Enemy enemy)
    {
        if (!enemy.CanSeePlayer())
        {
            enemy.FSM.ChangeState(new IdleState(),enemy);
        }
    }
    public override void Exit(Enemy enemy) { 
        base.Exit(enemy);
    }
}
