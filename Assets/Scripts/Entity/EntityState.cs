using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState : BaseState
{
    protected EntityController entity;
    protected EntityStateMachine entityStateMachine;

    public EntityState(EntityController entity, EntityStateMachine entityStateMachine) : base (entityStateMachine)
    {
        this.entity = entity;
    }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void FrameUpdate() { }

    public override void PhysicsUpdate() { }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType) { }
}
