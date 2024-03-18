using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "EnemyLogic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float randomMovementRange = 5f;
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private Vector3 _targetPos;
    private Vector3 _direction;

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

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEntity(_direction, movementSpeed);

        if ((_targetPos - enemy.transform.position).sqrMagnitude < 0.1f)
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

        return enemy.transform.position + new Vector3(newDirection.x, 0f, newDirection.y) * randomMovementRange;
    }
}
