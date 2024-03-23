using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "EnemyLogic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float randomMovementRange = 5f;

    [SerializeField] private Vector3 _targetPos;

    public override void DoAnimationTriggerEventLogic(EntityController.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        _targetPos = GetRandomPointInCircle();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Enemy.MoveToTarget(_targetPos, Enemy.MovementWalkSpeed);

        if ((_targetPos - Enemy.transform.position).sqrMagnitude < 0.1f)
        {
            _targetPos = GetRandomPointInCircle();
        }
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyController enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private Vector3 GetRandomPointInCircle()
    {
        Vector2 newDirection = UnityEngine.Random.insideUnitCircle;

        return Enemy.transform.position + new Vector3(newDirection.x, 0f, newDirection.y) * randomMovementRange;
    }
}
