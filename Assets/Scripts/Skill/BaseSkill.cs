using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : ScriptableObject
{
    [Header("Range Attributes")]
    public float range = 1f;
    public bool isOutside = false; // Check within range or outside range

    [Header("Skill Attributes")]
    public float cooldown = 0f;

    [Header("Movement Modifiers")]
    [Range(0, 1f)] public float movePercentage = 0f;

    [Header("Skill UI Attributes")]
    public Sprite artIcon;
    public AudioClip soundFX;

    public abstract void Attack(Animator animator);
    public abstract bool WithinRange(float targetRange);
}
