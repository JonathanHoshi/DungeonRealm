using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Vector3 Movement;

    [SerializeField] private GameObject cameraObj;

    private PlayerInput _playerInput;

    #region Input Fields

    private InputAction _moveAction;

    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManager();
        }
    }

    public void InitializeManager()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
    }

    private void Update()
    {
        Movement = ReadMovementValue();
    }

    private Vector3 ReadMovementValue()
    {
        Vector3 moveVector = V2xyToV3xz(_moveAction.ReadValue<Vector2>());
        Vector3 camForward = cameraObj.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return camForward * moveVector.z + cameraObj.transform.right * moveVector.x;
    }

    private Vector3 V2xyToV3xz(Vector2 inputVector)
    {
        return new Vector3(inputVector.x, 0f, inputVector.y);
    }
}
