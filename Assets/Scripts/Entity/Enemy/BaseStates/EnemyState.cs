using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityActiveState
{
    protected EnemyController Enemy { get { return (EnemyController)entity; } }

    public EnemyState(EnemyController enemy, EntityStateMachine entityStateMachine) 
        : base(enemy, entityStateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

}
