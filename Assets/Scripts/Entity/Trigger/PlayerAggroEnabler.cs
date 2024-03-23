using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAggroEnabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.SetAggroStatus(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.SetAggroStatus(false);
        }
    }
}
