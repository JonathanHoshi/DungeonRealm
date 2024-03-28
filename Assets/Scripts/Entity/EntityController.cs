using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EntitySpawnLocation
{
    public enum SpawnPoint
    {
        ProjFirePoint,
        BuffCastPoint,
    }
    public Transform projectileFirePoint;
    public Transform buffCastPoint;

    public Transform GetSpawnPoint(SpawnPoint spawnPoint)
    {
        switch (spawnPoint)
        {
            case SpawnPoint.ProjFirePoint:
                return projectileFirePoint;
            case SpawnPoint.BuffCastPoint:
                return projectileFirePoint;
        }

        return null;
    }
}

public abstract class EntityController : MonoBehaviour, IDamagable, IEntityMovable
{
    #region Damagable Variables
    [field: Header("Damagable Variables")]
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [field: SerializeField] public float CurrentHealth { get; set; }

    #endregion

    #region StateMachine Variables
    public EntityStateMachine StateMachine { get; set; }

    #endregion

    #region Movement Variables
    public Rigidbody RB { get; set; }
    public Animator Animator { get; set; }

    [field: Header("Movement Variables")]
    [field: SerializeField] public float RotationSpeed { get; set; } = 1.0f;
    [field: SerializeField] public float MovementWalkSpeed { get; set; } = 2.0f;
    [field: SerializeField] public float MovementSprintSpeed { get; set; } = 5.0f;

    #endregion

    #region Skill Variables

    [field: Header("Skill Variables")]
    [field: SerializeField] private List<SkillSOBase> SkillList { get; set; }
    public SkillSOBase[] SkillListInstance { get; set; }

    [field: SerializeField] public int CurrentSkillIndex { get; set; } = -1;

    [SerializeField] public LayerMask targetableMask;

    #endregion

    #region Modifier Variables

    [Header("Modifier Variables")]
    public float movementSpeedSkillModifier = 1f;
    public bool canMoveModifier = true;

    #endregion

    [Header("SpawnPoint Variables")]
    [SerializeField] protected EntitySpawnLocation spawnLocationList = new EntitySpawnLocation();

    public AnimatorOverrideController animatorOverrideController;

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();

        StateMachine = new EntityStateMachine();

        SkillListInstance = new SkillSOBase[SkillList.Count];

        for (int i = 0; i < SkillList.Count; i++)
        {
            SkillListInstance[i] = Instantiate(SkillList[i]);
        }
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;

        animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
        Animator.runtimeAnimatorController = animatorOverrideController;
        
        for (int i = 0; i < SkillList.Count; i++)
        {
            SkillListInstance[i].Initialize();
        }
    }

    protected virtual void Update()
    {
        if (StateMachine.IsInitialized())
        {
            StateMachine.CurrentEntityState.FrameUpdate();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (StateMachine.IsInitialized())
        {
            StateMachine.CurrentEntityState.PhysicsUpdate();
        }
    }

    #region Entity Health / Death Functions

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
    

    }
    #endregion

    #region Entity Movement Functions

    public virtual void MoveEntity(Vector3 directon, float speed)
    {
        RB.velocity = (canMoveModifier)
            ? movementSpeedSkillModifier * speed * directon
            : Vector3.zero;
    }

    public virtual void RotateEntity(Vector3 direction, float speed)
    {
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    //public void RotateEntity(Vector3 direction)
    //{
    //    Quaternion targetRotation = Quaternion.LookRotation(direction);

    //    targetRotation.x = transform.rotation.x;
    //    targetRotation.z = transform.rotation.z;

    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
    //}

    #endregion

    #region Entity Animation Triggers

    public enum AnimationTriggerType 
    { 
        UseSkill,
        EndSkill,
        EntityDamaged,
        PlayFootstepSound,
    }

    protected void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        if (StateMachine.IsInitialized()) 
        { 
            StateMachine.CurrentEntityState.AnimationTriggerEvent(triggerType);
        }
    }

    #endregion

    public abstract Vector3 GetTargetPosition();

    public void UseSkillEvent()
    {
        if (CurrentSkillIndex != -1)
        {
            EntitySkillInfo entitySkillInfo = new EntitySkillInfo(targetableMask, 1f, GetTargetPosition());

            SkillList[CurrentSkillIndex].UseSkillEvent(this, entitySkillInfo);
        }
    }

    public void EndSkillEvent()
    {
        if (CurrentSkillIndex != -1)
        {
            SkillList[CurrentSkillIndex].EndSkillEvent(this);
        }

        CurrentSkillIndex = -1;
    }

    public Transform GetSpawnTransform(EntitySpawnLocation.SpawnPoint spawnPoint)
    {
        Transform result = spawnLocationList.GetSpawnPoint(spawnPoint);

        return (result != null) ? result : transform;
    }
}
