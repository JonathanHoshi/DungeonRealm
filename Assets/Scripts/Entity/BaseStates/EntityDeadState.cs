using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeadState : EntityState
{
    public EntityDeadState(EntityController entity, EntityStateMachine entityStateMachine) 
        : base(entity, entityStateMachine) { }
}
