using MagicArsenal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileFX : MagicProjectileScript
{
    [Header("Skill Variables")]
    [SerializeField] protected LayerMask hitLayer;

    protected float damage = 1f; 

    public void InitializeProjectile(EntitySkillInfo entitySkillInfo)
    {
        hitLayer = hitLayer | entitySkillInfo.targetMask;
        damage = entitySkillInfo.damage;
    }

    protected override (bool, RaycastHit) CheckHitObject(Vector3 position, float rad, Vector3 dir, float dist)
    {
        return (Physics.SphereCast(position, rad, dir, out var hit, dist, hitLayer), hit);
    }

    protected override void HandleHitEvent(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent<IDamagable>(out var hitEntity)) 
        {
            hitEntity.Damage(damage);
        }
    }
}
