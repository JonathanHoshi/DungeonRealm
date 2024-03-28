using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Range Attack", menuName = "Skill/Attack/Range/Default Range")]
public class RangeSOBase : SkillSOBase
{
    [Header("Range Attributes")]
    public GameObject projectilePrefab;
    public float projectileMaxRange = 20f;
    public float projectileSpeed = 2f;

    public override void UseSkillEvent(EntityController entity, EntitySkillInfo entitySkillInfo)
    {
        Vector3 spawnPosition = entity.GetSpawnTransform(EntitySpawnLocation.SpawnPoint.ProjFirePoint).position;
        Vector3 direction = entitySkillInfo.targetPosition - entity.transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.transform.LookAt(spawnPosition + direction);
        projectile.GetComponent<Rigidbody>().AddForce(direction * projectileSpeed * 10f);
        projectile.GetComponent<ProjectileFX>().InitializeProjectile(entitySkillInfo);
    }
}
