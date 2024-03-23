using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aggro-Default Aggro", menuName = "EnemyLogic/Aggro Logic/Default Aggro")]
public class EnemyAggroDefault : EnemyAggroSOBase
{
    [Header("Obstacles Variables")]
    [SerializeField] [Range(0f, 1f)] private float ignoreObstaclesAggroRange = 0.5f;
    [SerializeField] private LayerMask aggroObstacleMask;

    protected override bool CheckPlayerAggroStatus()
    {
        return base.CheckPlayerAggroStatus() && CheckObstacleBetweenPlayer();
    }

    private bool CheckObstacleBetweenPlayer()
    {
        return (AILogic.IsWithinSqrDistance(Enemy.transform.position, PlayerTransform.position, 
            ignoreObstaclesAggroRange * combatAggroRadius)
            || AILogic.CheckNoObstaclesBetween(Enemy.transform.position, PlayerTransform.position, aggroObstacleMask));
    }
}
