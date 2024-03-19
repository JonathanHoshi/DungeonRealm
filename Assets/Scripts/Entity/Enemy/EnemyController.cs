using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : EntityController
{
    #region StateMachine Variables

    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }

    #endregion

    #region ScriptableObject Variables

    [Header("State Variables")]
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyAggroSOBase EnemyAggroBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyAggroSOBase EnemyAggroBaseInstance { get; set; }

    #endregion

    [field: Header("Aggro Variables")]
    [field: SerializeField] public bool IsAggroEnabled { get; set; }
    [field: SerializeField] public bool IsWithinChaseRange {  get; set; }
    [field: SerializeField] public bool IsWithinAttackRange { get; set; }

    private Coroutine aggroCoroutine = null;

    protected override void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyAggroBaseInstance = Instantiate(EnemyAggroBase);

        base.Awake();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyAggroBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool IsAttackState()
    {
        return StateMachine.CurrentEntityState == AttackState;
    }

    public bool IsChaseState()
    {
        return StateMachine.CurrentEntityState == ChaseState;
    }

    public bool IsIdleState()
    {
        return StateMachine.CurrentEntityState == IdleState;
    }


    public void StartAggroCoroutine(IEnumerator aggroHandler)
    {
        if (aggroCoroutine != null)
        {
            StopAggroCoroutune();
        }
        StartCoroutine(aggroHandler);
    }

    public void StopAggroCoroutune()
    {
        if (aggroCoroutine != null)
        {
            StopCoroutine(aggroCoroutine);
            aggroCoroutine = null;
        }
    }
}
