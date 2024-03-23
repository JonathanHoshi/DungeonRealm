using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySOBase : ScriptableObject
{
    protected EnemyController Enemy { get; private set; }
    protected Transform Transform { get; private set; }
    protected GameObject GameObject { get; private set; }

    protected Transform PlayerTransform { get; private set; }

    public virtual void Initialize(GameObject gameObject, EnemyController enemy)
    {
        this.GameObject = gameObject;
        Transform = gameObject.transform;
        this.Enemy = enemy;

        PlayerTransform = (GameManager.instance != null && GameManager.instance.PlayerRef != null) 
            ? GameManager.instance.PlayerRef.transform
            : GameObject.FindGameObjectWithTag("Player").transform;
    }
}
