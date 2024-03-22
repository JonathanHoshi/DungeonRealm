using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Input Variables")]
    public Vector3 Movement;
    public Vector2 Mouse;
    public Vector3 MouseWorld;
    public Vector3 LookDirection;

    [Header("Mouse Cursor Variables")]
    [SerializeField] private GameObject cursorObj;
    [SerializeField] private float mouseHeightPos = 0f;

    [Header("Camera Variables")]
    [SerializeField] private GameObject cameraObj;

    #region ControlScheme Variables

    // Ensure enum and control scheme int value is correct
    public enum InputControlType
    {
        MOUSEKEYBOARD = 0,
        CONTROLLER = 1,
    }

    public InputControlType ControlType;

    #endregion

    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Plane _mousePlane;

    #region Input Fields

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _mouseAction;

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
        _lookAction = _playerInput.actions["Look"];
        _mouseAction = _playerInput.actions["Mouse"];
        _mainCamera = cameraObj.GetComponent<Camera>();
        _mousePlane = new Plane(Vector3.up, new Vector3(0, mouseHeightPos, 0));

        SetCurrentControlScheme(_playerInput.currentControlScheme);
    }

    private void Update()
    {
        Movement = ReadMovementValue();

        HandleLookDirection();
    }

    private void HandleLookDirection()
    {
        if (ControlType == InputControlType.MOUSEKEYBOARD)
        {
            Mouse = _mouseAction.ReadValue<Vector2>();
            MouseWorld = ScreenToWorldPosition(_mainCamera, Mouse);
            cursorObj.transform.position = MouseWorld;
            LookDirection = GetPlayerLookDirection(MouseWorld);
        }

    }

    private Vector3 GetPlayerLookDirection(Vector3 worldPosition)
    {
        Vector3 playerPosition = MouseWorld - GameManager.instance.PlayerRef.transform.position;
        playerPosition.y = 0;

        return playerPosition.normalized;
    }

    private Vector3 ScreenToWorldPosition(Camera cam, Vector2 mousePosition)
    {
        Ray camRay = cam.ScreenPointToRay(mousePosition);

        _mousePlane.Raycast(camRay, out var distToPoint);

        return camRay.GetPoint(distToPoint);
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

    #region Control Scheme Settings

    void OnEnable()
    {
        InputUser.onChange += onInputDeviceChange;
    }

    void OnDisable()
    {
        InputUser.onChange -= onInputDeviceChange;
    }

    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            SetCurrentControlScheme(user.controlScheme.Value.name);
        }
    }

    private void SetCurrentControlScheme(string controlScheme)
    {
        for (int i = 0; i < _playerInput.currentActionMap.controlSchemes.Count; i++)
        {
            if (controlScheme == _playerInput.currentActionMap.controlSchemes[i].name)
            {
                ControlType = (InputControlType)i;
                break;
            }
        }
    }

    #endregion
}
