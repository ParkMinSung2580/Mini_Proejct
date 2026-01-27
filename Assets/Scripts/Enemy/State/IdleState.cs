using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState<Enemy> 
{
    public override void Enter(Enemy enemy) { base.Enter(enemy); enemy.FSM.ChangeState(new PatrolState(), enemy); }
    public override void Update(Enemy enemy) { }
    public override void Exit(Enemy enemy) { base.Exit(enemy); }
}
