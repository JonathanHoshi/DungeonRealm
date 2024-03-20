using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController enemy, EntityStateMachine entityStateMachine) : base(enemy, entityStateMachine) { }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        Enemy.EnemyIdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Enemy.EnemyIdleBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        Enemy.EnemyIdleBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        if (Enemy.IsAggroEnabled)
        {
            if (Enemy.IsWithinAttackRange)
            {
                Enemy.StateMachine.ChangeState(Enemy.AttackState);
            }
            else if (Enemy.IsWithinChaseRange)
            {
                Enemy.StateMachine.ChangeState(Enemy.ChaseState);
            }
        }

        Enemy.EnemyIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Enemy.EnemyIdleBaseInstance.DoPhysicsUpdateLogic();
    }
}
