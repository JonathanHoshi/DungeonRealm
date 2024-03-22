using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Aggro-Base Aggro", menuName = "EnemyLogic/Aggro Logic/Base Aggro")]
public class EnemyAggroSOBase : EnemySOBase
{
    [SerializeField] private float chaseAggroRange = 10f;
    [SerializeField] private float attackAggroRange = 2f;

    [SerializeField] private int idleAggroChecksPerSecond = 2;
    [SerializeField] private int chaseAggroChecksPerSecond = 4;
    [SerializeField] private int attackAggroChecksPerSecond = 8;

    [SerializeField] private LayerMask aggroObstacleMask;
    [SerializeField] private bool aggroChecksIgnoreObstacles = false;
    [SerializeField] private bool aggroChecksIgnoreEnabled = false;

    private float currentAggroTimer;

    public override void Initialize(GameObject gameObject, EnemyController enemy)
    {
        base.Initialize(gameObject, enemy);

        InitializeAggroCoroutune();
    }

    public virtual void DoEnterLogic() 
    {
        InitializeAggroCoroutune();
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    {
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void ResetValues() 
    {
        enemy.StopAggroCoroutune();
    }

    protected virtual IEnumerator AggroHandler()
    {
        while(true)
        {
            HandleAggroLogic();

            UpdateCurrentAggroTimer();
            yield return new WaitForSeconds(currentAggroTimer);
        }
    }

    protected virtual void HandleAggroLogic() 
    {
        if (!enemy.IsAggroEnabled && !aggroChecksIgnoreEnabled)
        {
            return;
        }

        enemy.IsWithinChaseRange = CheckIsWithinChaseDistance(chaseAggroRange);
        enemy.IsWithinAttackRange = CheckIsWithinAttackDistance(attackAggroRange);
    }

    private bool CheckIsWithinChaseDistance(float distance)
    {
        return (IsWithinSqrDistance(enemy.transform.position, playerTransform.position, distance)
        && (aggroChecksIgnoreObstacles || CheckNoObstaclesBetween(enemy.transform.position, playerTransform.position)));
    }

    private bool CheckIsWithinAttackDistance(float distance)
    {
        return IsWithinSqrDistance(enemy.transform.position, playerTransform.position, distance);
    }

    private bool IsWithinSqrDistance(Vector3 a, Vector3 b, float distance)
    {
        return Vector3.SqrMagnitude(a - b) <= distance * distance;
    }
    private bool CheckNoObstaclesBetween(Vector3 origin, Vector3 target)
    {
        return !Physics.Raycast(origin, target - origin, Vector3.Distance(origin, target), aggroObstacleMask);
    }

    private void InitializeAggroCoroutune()
    {
        UpdateCurrentAggroTimer();

        enemy.StartAggroCoroutine(AggroHandler());
    }
    private void UpdateCurrentAggroTimer()
    {
        currentAggroTimer = (enemy.IsIdleState())
            ? 1.0f / idleAggroChecksPerSecond
            : (enemy.IsChaseState())
                ? 1.0f / chaseAggroChecksPerSecond
                : 1.0f / attackAggroChecksPerSecond;
    }
}
