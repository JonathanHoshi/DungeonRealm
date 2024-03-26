using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combat-Default Combat", menuName = "EnemyLogic/Combat Logic/Default Combat")]
public class EnemyCombatSOBase : EnemySOBase
{
    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        Enemy.HandleSkillSelectionUpdateLogic(PlayerTransform);

        switch (Enemy.AIMovementPreference)
        {
            case AISkillMovementPreference.Idle:
                break;
            case AISkillMovementPreference.MoveTowards:
                Enemy.MoveToTarget(PlayerTransform.position, Enemy.MovementSprintSpeed);
                break;
            case AISkillMovementPreference.MoveAway:
                Enemy.MoveToTarget(Enemy.transform.position - PlayerTransform.position, Enemy.MovementSprintSpeed);
                break;
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyController.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
