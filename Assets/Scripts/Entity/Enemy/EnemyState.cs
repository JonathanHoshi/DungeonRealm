using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityState
{
    protected EnemyController enemy { get { return (EnemyController)entity; } }

    public EnemyState(EnemyController entity, BaseStateMachine entityStateMachine) : base(entity, entityStateMachine) { }
}
