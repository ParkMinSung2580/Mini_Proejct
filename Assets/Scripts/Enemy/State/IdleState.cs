using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyStateBase
{
    public override void Enter(Enemy enemy) { Debug.Log("Idle 시작"); }
    public override void Update(Enemy enemy)
    {
        // 캐시된 값만 사용
        //if (enemy.CanSeePlayerCached)
        //{
            enemy.ChangeState(new ChaseState());
        //}
    }
    public override void Exit(Enemy enemy) { Debug.Log("Idle 종료"); }
}
