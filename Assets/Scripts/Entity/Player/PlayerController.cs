using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : EntityController
{
    public PlayerCombatState CombatState { get; set; }

    [SerializeField] private LayerMask groundMask;

    protected override void Awake()
    {
        base.Awake();

        CombatState = new PlayerCombatState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(CombatState);
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override Vector3 GetTargetPosition()
    {
        return InputManager.instance.MouseWorld;
    }
}
