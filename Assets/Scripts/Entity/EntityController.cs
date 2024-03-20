using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();

        StateMachine = new EntityStateMachine();
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
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
        RB.velocity = directon * speed;
    }

    public void RotateEntity(Vector3 direction)
    {
        float singleStep = RotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    #endregion

    #region Entity Animation Triggers

    public enum AnimationTriggerType 
    { 
        EntityDamaged,
        PlayFootstepSound,
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEntityState.AnimationTriggerEvent(triggerType);
    }


    #endregion


}
