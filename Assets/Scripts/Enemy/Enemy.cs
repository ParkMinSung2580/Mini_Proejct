using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private IEnemyState currentState; 

    private void Start() 
    { 
        ChangeState(new IdleState()); 
    }

    void Update() { currentState?.Update(this); }

    public void ChangeState(IEnemyState newState) 
    { 
        currentState?.Exit(this); 
        currentState = newState; 
        currentState.Enter(this); 
    }

    public bool CanSeePlayer()
    {
        return false;
    }
}
