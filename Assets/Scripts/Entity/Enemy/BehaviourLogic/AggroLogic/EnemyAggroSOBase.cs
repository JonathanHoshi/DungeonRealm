using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAggroSOBase : EnemySOBase
{
    [Header("Override Variables")]
    [SerializeField] private bool ignoreAggroChecks = false;

    [Header("Aggro Variables")]
    [SerializeField] protected float combatAggroRadius = 10f;

    [Header("Aggro Check Variables")]
    [SerializeField] private int idleAggroChecksPerSecond = 2;
    [SerializeField] private int combatAggroChecksPerSecond = 4;


    private float currentAggroTimer;

    public override void Initialize(GameObject gameObject, EnemyController enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public virtual void DoStartLogic() 
    {
        if (!ignoreAggroChecks) {
            InitializeAggroCoroutine();
        }
    }

    public virtual void DoStopLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void ResetValues() 
    {
        Enemy.StopAggroCoroutune();
    }

    private IEnumerator AggroHandler()
    {
        while (true)
        {
            HandleAggroLogic();

            UpdateCurrentAggroTimer();
            yield return new WaitForSeconds(currentAggroTimer);
        }
    }

    private void HandleAggroLogic()
    {
        if (Enemy.IsAggroEnabled)
        {
            Enemy.IsPlayerAggro = CheckPlayerAggroStatus();
        }
    }

    protected virtual bool CheckPlayerAggroStatus()
    {
        return AILogic.IsWithinSqrDistance(Enemy.transform.position, PlayerTransform.position, combatAggroRadius);
    }

    private void InitializeAggroCoroutine()
    {
        UpdateCurrentAggroTimer();

        Enemy.StartAggroCoroutine(AggroHandler());
    }
    private void UpdateCurrentAggroTimer()
    {
        currentAggroTimer = (Enemy.IsIdleState())
            ? 1.0f / idleAggroChecksPerSecond
            : 1.0f / combatAggroChecksPerSecond;
    }
}
