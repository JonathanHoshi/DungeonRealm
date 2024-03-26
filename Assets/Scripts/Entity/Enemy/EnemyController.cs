using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class EnemyController : EntityController
{
    #region StateMachine Variables

    public EnemyIdleState IdleState { get; set; }
    public EnemyCombatState CombatState { get; set; }

    #endregion

    #region ScriptableObject Variables

    [Header("State Variables")]
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyCombatSOBase EnemyCombatBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyCombatSOBase EnemyCombatBaseInstance { get; set; }

    #endregion

    #region Aggro Variables

    [Header("Aggro Variables")]
    [SerializeField] private EnemyAggroSOBase EnemyAggroBase;

    public EnemyAggroSOBase EnemyAggroBaseInstance { get; set; }
    [field: SerializeField] public bool IsAggroEnabled { get; private set; }
    [field: SerializeField] public bool IsPlayerAggro {  get; set; }

    private Coroutine aggroCoroutine = null;

    #endregion

    #region AI Avoidance Variables

    [field: Header("AI Avoidance Variables")]
    [SerializeField] private LayerMask aiAvoidLayer;
    [SerializeField] private float force = 20.0f;
    [SerializeField] private float minimumDistToAvoid = 2f;

    #endregion

    public AISkillMovementPreference AIMovementPreference { get; set; } = AISkillMovementPreference.Idle;

    protected override void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyCombatBaseInstance = Instantiate(EnemyCombatBase);
        EnemyAggroBaseInstance = Instantiate(EnemyAggroBase);

        base.Awake();

        IdleState = new EnemyIdleState(this, StateMachine);
        CombatState = new EnemyCombatState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyCombatBaseInstance.Initialize(gameObject, this);
        EnemyAggroBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        Animator.SetFloat("MoveSpeed", Vector3.Magnitude(RB.velocity));

        EnemyAggroBaseInstance.DoFrameUpdateLogic();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        EnemyAggroBaseInstance.DoPhysicsUpdateLogic();
    }

    public bool IsCombatState()
    {
        return StateMachine.CurrentEntityState == CombatState;
    }

    public bool IsIdleState()
    {
        return StateMachine.CurrentEntityState == IdleState;
    }

    public void MoveToTarget(Vector3 targetPosition, float speed)
    {
        MoveEntity(transform.forward, speed);

        Vector3 calculatedDirection = AvoidObstacles(targetPosition, aiAvoidLayer).normalized;
        RotateEntity(calculatedDirection, RotationSpeed);
    }

    private Vector3 AvoidObstacles(Vector3 targetPosition, LayerMask layer)
    {
        //Check that the vehicle hit with the obstacles within it's minimum distance to avoid
        Vector3 right45 = (transform.forward + transform.right).normalized;
        Vector3 left45 = (transform.forward - transform.right).normalized;
        Vector3 startPos = transform.position + transform.up * 1;
        if (Physics.Raycast(startPos, right45, minimumDistToAvoid, layer))
        {
            //Get the new directional vector by adding force to vehicle's current forward vector
            return transform.forward - transform.right * force;
        }
        else if (Physics.Raycast(startPos, left45, minimumDistToAvoid, layer))
        {
            //Get the new directional vector by adding force to vehicle's current forward vector
            return transform.forward + transform.right * force;
        }
        else if (Physics.Raycast(startPos, transform.forward, out RaycastHit Hit, minimumDistToAvoid, layer))
        {
            //Get the normal of the hit point to calculate the new direction
            Vector3 hitNormal = Hit.normal;
            hitNormal.y = 0.0f; //Don't want to move in Y-Space
            return transform.forward + hitNormal * force;
        }
        else
        {
            return targetPosition - transform.position;
        }
    }

    #region Aggro Functions

    public void SetAggroStatus(bool status)
    {
        IsAggroEnabled = status;

        if (IsAggroEnabled)
        {
            EnemyAggroBaseInstance.DoStartLogic();
        } else
        {
            EnemyAggroBaseInstance.DoStopLogic();
        }
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

    #endregion


    public void HandleSkillSelectionUpdateLogic(Transform target)
    {
        if (CurrentSkillIndex == -1)
        {
            int skillReadyIndex = -1;
            int skillMovementPreferenceIndex = -1;

            for (int i = 0; i < SkillListInstance.Length; i++)
            {
                if (SkillListInstance[i].IsSkillReady()) {
                    if (SkillListInstance[i].WithinRange(transform, target))
                    {
                        skillReadyIndex = i;
                        break;
                    }

                    // Select highest cooldown skill with a movement preference for movement logic
                    if (SkillListInstance[i].GetMovementPreference() != AISkillMovementPreference.Idle 
                        && (skillMovementPreferenceIndex == -1 || SkillListInstance[i].cooldown > SkillListInstance[skillMovementPreferenceIndex].cooldown))
                    {
                        skillMovementPreferenceIndex = i;
                    }
                }
            }

            if (skillMovementPreferenceIndex != -1)
            {
                AIMovementPreference = SkillListInstance[skillMovementPreferenceIndex].GetMovementPreference();
            } 
            else
            {
                AIMovementPreference = AISkillMovementPreference.Idle;
            }

            if (skillReadyIndex != -1)
            {
                CurrentSkillIndex = skillReadyIndex;
                SkillListInstance[skillReadyIndex].DoSkillLogic(this);

                AIMovementPreference = SkillListInstance[skillReadyIndex].GetMovementPreference();
            }
        }
    }
}
