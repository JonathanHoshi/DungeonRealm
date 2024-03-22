using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityState
{
    protected EnemyController Enemy { get { return (EnemyController)entity; } }

    public EnemyState(EnemyController enemy, EntityStateMachine entityStateMachine) : base(enemy, entityStateMachine) { }
}
