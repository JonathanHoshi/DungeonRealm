using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Player Chase", menuName = "EnemyLogic/Chase Logic/Player Chase")]
public class EnemyChasePlayer : EnemyChaseSOBase
{
    public override void DoAnimationTriggerEventLogic(EntityController.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyController enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
