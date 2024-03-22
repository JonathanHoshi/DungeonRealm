using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemy, EntityStateMachine entityStateMachine) : base(enemy, entityStateMachine) { }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        Enemy.EnemyAttackBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Enemy.EnemyAttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        Enemy.EnemyAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        if (Enemy.IsAggroEnabled)
        {
            if (!Enemy.IsWithinChaseRange)
            {
                Enemy.StateMachine.ChangeState(Enemy.IdleState);
            }
            else if (!Enemy.IsWithinAttackRange)
            {
                Enemy.StateMachine.ChangeState(Enemy.ChaseState);
            }
        }

        Enemy.EnemyAttackBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Enemy.EnemyAttackBaseInstance.DoPhysicsUpdateLogic();
    }
}
