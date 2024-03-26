using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerState : EntityState
{
    protected PlayerController Player { get { return (PlayerController)entity; } }

    public PlayerState(PlayerController player, EntityStateMachine entityStateMachine) 
        : base(player, entityStateMachine) { }
}
