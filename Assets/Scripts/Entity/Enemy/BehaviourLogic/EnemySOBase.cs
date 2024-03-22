using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySOBase : ScriptableObject
{
    protected EnemyController enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, EnemyController enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = (GameManager.instance != null && GameManager.instance.PlayerRef != null) 
            ? GameManager.instance.PlayerRef.transform
            : GameObject.FindGameObjectWithTag("Player").transform;
    }
}
