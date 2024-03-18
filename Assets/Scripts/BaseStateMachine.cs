using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine
{
    protected BaseState CurrentState { get; set; }

    public void Initialize(BaseState initialState)
    {
        CurrentState = initialState;
        CurrentState.EnterState();
    }

    public void ChangeState(BaseState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
