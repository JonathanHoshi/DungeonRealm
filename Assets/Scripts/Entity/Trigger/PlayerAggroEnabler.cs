using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAggroEnabler : MonoBehaviour
{
    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.IsAggroEnabled = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.IsAggroEnabled = false;
        }
    }
}
