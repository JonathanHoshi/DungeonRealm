using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SkillClipOverride
{
    public enum SkillState
    {
        MainSkill,
    }

    public AnimationClip clip;
    public SkillState overrideState;
}

public enum AISkillMovementPreference
{
    Idle,
    MoveTowards,
    MoveAway,
}

public abstract class SkillSOBase : ScriptableObject
{
    [Header("AI Range Attributes")]
    public float skillRange = 2f;
    public bool isOutside = false; // Check within range or outside range

    [Header("Skill Attributes")]
    public float cooldown = 0f;
    public bool startOnCooldown = false;

    [Header("Movement Modifiers")]
    [Range(-5, 5f)] public float movementSpeedIncreasePercentage = 0f;
    public bool cannotMove = true;

    [Header("Skill Animation Variables")]
    public SkillClipOverride skillClipOverride;

    [Header("Skill UI Attributes")]
    public Sprite artIcon;
    public AudioClip soundFX;

    private float cooldownTimer = 0f;

    public virtual void Initialize()
    {
        if (startOnCooldown)
        {
            cooldownTimer = cooldown;
        }
    }

    public void DoSkillLogic(EntityController entity)
    {
        if (IsSkillReady())
        {
            ResetSkill();
            entity.animatorOverrideController[skillClipOverride.overrideState.ToString()] = skillClipOverride.clip;
            entity.Animator.SetTrigger("UseSkill");

            entity.canMoveModifier = !cannotMove;

            if (!cannotMove)
                entity.movementSpeedSkillModifier += movementSpeedIncreasePercentage;
        }
    }

    public virtual void DoFrameUpdateLogic()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public virtual bool IsSkillReady()
    {
        return (cooldownTimer <= 0f);
    }

    public virtual void ResetSkill()
    {
        cooldownTimer = cooldown;
    }

    public AISkillMovementPreference GetMovementPreference()
    {
        return (skillRange == 0)
            ? AISkillMovementPreference.Idle
            : (!isOutside)
                ? AISkillMovementPreference.MoveTowards
                : AISkillMovementPreference.MoveAway;
    }

    public virtual bool WithinRange(Transform entity, Transform target)
    {
        float targetSqrDist = Vector3.SqrMagnitude(entity.transform.position - target.transform.position);
        bool isTargetInside = targetSqrDist <= skillRange * skillRange;

        return !isOutside
            ? isTargetInside
            : !isTargetInside;
    }
    public abstract void UseSkillEvent(EntityController entity);

    public virtual void EndSkillEvent(EntityController entity)
    {
        entity.canMoveModifier = true;

        if (!cannotMove)
            entity.movementSpeedSkillModifier -= movementSpeedIncreasePercentage;
    }
}
