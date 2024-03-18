using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected BaseStateMachine baseStateMachine;

    public BaseState(BaseStateMachine baseStateMachine)
    {
        this.baseStateMachine = baseStateMachine;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void FrameUpdate();
    public abstract void PhysicsUpdate();
    public abstract void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType);

}
