using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Melee Attack", menuName = "Skill/Attack/Melee/Default Melee")]
public class MeleeSOBase : SkillSOBase
{
    [Header("Melee Attributes")]
    public float meleeAngle = 1f;

    public override void UseSkillEvent(EntityController entity, EntitySkillInfo entitySkillInfo)
    {
        Collider[] hitColliders = Physics.OverlapSphere(entity.transform.position, skillRange, entitySkillInfo.targetMask);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IDamagable>(out IDamagable hitEntity)
                && WithinAngle(entity.transform, hitCollider.transform))
            {
                hitEntity.Damage(entitySkillInfo.damage);
            }
        }
    }

    private bool WithinAngle(Transform entity, Transform target)
    {
        Vector3 targetDir = target.position - entity.transform.position;

        return Vector3.Angle(entity.transform.forward, targetDir) <= meleeAngle;
    }
}
