using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyStateBase
{
    public IdleState(Enemy enemy, StateMachine fsm) : base(enemy, fsm) { }

    public override void Enter() { base.Enter(); }
    public override void Update()
    {

    }
    public override void Exit() { base.Exit(); }
}
