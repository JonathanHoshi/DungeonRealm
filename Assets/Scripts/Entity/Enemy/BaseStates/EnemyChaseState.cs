using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemy, EntityStateMachine entityStateMachine) : base(enemy, entityStateMachine) { }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        Enemy.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Enemy.EnemyChaseBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        Enemy.EnemyChaseBaseInstance.DoExitLogic();
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
            else if (Enemy.IsWithinAttackRange)
            {
                Enemy.StateMachine.ChangeState(Enemy.AttackState);
            }
        }

        Enemy.EnemyChaseBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Enemy.EnemyChaseBaseInstance.DoPhysicsUpdateLogic();
    }
}
