using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : EnemyState
{
    public EnemyCombatState(EnemyController enemy, EntityStateMachine entityStateMachine) 
        : base(enemy, entityStateMachine) { }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        Enemy.EnemyCombatBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Enemy.EnemyCombatBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        Enemy.EnemyCombatBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!Enemy.IsPlayerAggro)
        {
            Enemy.StateMachine.ChangeState(Enemy.IdleState);
        }

        Enemy.EnemyCombatBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Enemy.EnemyCombatBaseInstance.DoPhysicsUpdateLogic();
    }
}