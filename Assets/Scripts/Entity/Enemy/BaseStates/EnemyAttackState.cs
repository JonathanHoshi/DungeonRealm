using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemy, BaseStateMachine entityStateMachine) : base(enemy, entityStateMachine) { }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyAttackBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.EnemyAttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.EnemyAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.EnemyAttackBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyAttackBaseInstance.DoPhysicsUpdateLogic();
    }
}
