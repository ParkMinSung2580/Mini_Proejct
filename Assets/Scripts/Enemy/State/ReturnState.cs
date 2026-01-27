using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : EnemyStateBase
{ 
    public ReturnState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm) { }

    public override void Enter() { Debug.Log("Idle 시작"); }
    public override void Update()
    {

    }
    public override void Exit() { Debug.Log("Idle 종료"); }
}
