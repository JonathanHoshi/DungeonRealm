using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerState
{

    public PlayerCombatState(PlayerController player, EntityStateMachine entityStateMachine) : base(player, entityStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EntityController.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

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

        Player.MoveEntity(InputManager.instance.Movement, Player.MovementSprintSpeed);
        Player.RotateEntity(InputManager.instance.LookDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
