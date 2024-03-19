using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector3 Movement;

    private PlayerInput _playerInput;

    #region Input Fields

    private InputAction _moveAction;

    #endregion

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
    }

    private void Update()
    {
        Movement = V2xyToV3xz(_moveAction.ReadValue<Vector2>());
    }

    private Vector3 V2xyToV3xz(Vector2 inputVector)
    {
        return new Vector3(inputVector.x, 0f, inputVector.y);
    }
}
