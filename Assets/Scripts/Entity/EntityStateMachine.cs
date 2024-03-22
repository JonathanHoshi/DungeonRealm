using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateMachine : BaseStateMachine
{
    public EntityState CurrentEntityState { get { return (EntityState)CurrentState; } set { CurrentState = value; } }
}
