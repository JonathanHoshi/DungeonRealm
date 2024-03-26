using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActiveState : EntityState
{
    public EntityActiveState(EntityController entity, EntityStateMachine entityStateMachine) 
        : base(entity, entityStateMachine) { }

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

        if (entity.SkillListInstance != null)
        {
            for (int i = 0; i < entity.SkillListInstance.Length; i++)
            {
                entity.SkillListInstance[i].DoFrameUpdateLogic();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        switch (triggerType)
        {
            case EntityController.AnimationTriggerType.UseSkill:
                entity.UseSkillEvent();
                break;
            case EntityController.AnimationTriggerType.EndSkill:
                entity.EndSkillEvent();
                break;
            case EntityController.AnimationTriggerType.EntityDamaged:
                // Do animation damaged stuff
                break;
            case EntityController.AnimationTriggerType.PlayFootstepSound:
                break;
        }
    }
}
