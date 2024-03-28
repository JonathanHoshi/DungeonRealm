using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntitySkillInfo
{
    public LayerMask targetMask;
    public float damage;
    public Vector3 targetPosition;

    public EntitySkillInfo(LayerMask targetMask, float damage, Vector3 targetPosition)
    {
        this.targetMask = targetMask;
        this.damage = damage;
        this.targetPosition = targetPosition;
    }
}
