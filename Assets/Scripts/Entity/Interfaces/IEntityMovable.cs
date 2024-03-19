using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityMovable
{
    float MovementWalkSpeed { get; set; }
    float MovementSprintSpeed { get; set; } 
    float RotationSpeed { get; set; }
    Rigidbody RB { get; set; }
    Animator Animator { get; set; }

    void MoveEntity(Vector3 velocity, float speed);

    void RotateEntity(Vector3 direction);
}
