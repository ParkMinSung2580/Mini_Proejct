using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateContext<T>
{
    public StateMachine<T> FSM { get; }
}
