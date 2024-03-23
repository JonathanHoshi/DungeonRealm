using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AILogic
{
    public static bool IsWithinSqrDistance(Vector3 a, Vector3 b, float distance)
    {
        return Vector3.SqrMagnitude(a - b) <= distance * distance;
    }
    public static bool CheckNoObstaclesBetween(Vector3 origin, Vector3 target, LayerMask aggroObstacleMask)
    {
        return !Physics.Raycast(origin, target - origin, Vector3.Distance(origin, target), aggroObstacleMask);
    }
}
