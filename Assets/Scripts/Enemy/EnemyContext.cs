using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext
{
    public EnemyController Controller { get; }
    public EnemyAnimator Animator { get; }
    public EnemyData Data { get; }

    public EnemyContext(Enemy enemy)
    {
        Controller = enemy.GetComponent<EnemyController>();
        Animator = enemy.GetComponent<EnemyAnimator>();
        Data = enemy.Data;
    }
}
