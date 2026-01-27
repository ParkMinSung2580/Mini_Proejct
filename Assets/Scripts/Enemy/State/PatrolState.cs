using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState<Enemy>
{
    public override void Enter(Enemy enemy)
    {
        Debug.Log("Patrol 시작");
        enemy.animator.SetBool("IsMove", true);
    }
    public override void Update(Enemy enemy)
    {
        if (enemy.CanSeePlayer())
        {
            enemy.FSM.ChangeState(new ChaseState(),enemy);
        }
    }
    public override void Exit(Enemy enemy) 
    {
        Debug.Log("Patrol 종료");
    }
}
