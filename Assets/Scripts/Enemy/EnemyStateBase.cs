using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : IEnemyState
{
    public virtual void Enter(Enemy enemy) { }
    public virtual void Update(Enemy enemy) { }
    public virtual void Exit(Enemy enemy) { }
}
