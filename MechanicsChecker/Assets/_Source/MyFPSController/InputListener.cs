using System;
using _Source.MyFPSController;
using UnityEngine;
using Zenject;

public class InputListener : MonoBehaviour
{
    [field: Header("Mouse Settings")]
    [field: SerializeField]
    public bool CursorLocked { get; private set; } = true;

    [field: Header("Mouse Sensitivity")]
    [field: SerializeField]
    public float MouseSensX { get; private set; } = 300f;

    [field: SerializeField] public float MouseSensY { get; private set; } = 300f;

    [field: Header("Camera Look Clamp")]
    [field: SerializeField]
    public float TopCameraLimit { get; private set; } = 90f;

    [field: SerializeField] public float BottomCameraLimit { get; private set; } = -90f;

    //Dependencies
    public PlayerInvoker _playerInvoker;
    private PlayerControls _playerControls;
    private Camera _playerCamera;

    //Move Input;
    public Vector2 PlayerMovement { get; private set; }
    private float _verticalMove;
    private float _horizontalMove;

    public delegate void MoveInputEvent(Vector2 moveInput);
    public event MoveInputEvent OnMoveInput;

    //Look Input
    public float VerticalCameraMove { get; private set; }
    public float HorizontalCameraMove{ get; private set; }

    public delegate void CameraLookInputEvent(float vertical, float horizontal);
    public event CameraLookInputEvent OnCameraLookInput;

    [Inject]
    public void Initialise(PlayerControls playerControls, Camera playerCamera, PlayerInvoker playerInvoker)
    {
        _playerControls = playerControls;
        _playerCamera = playerCamera;
        _playerInvoker = playerInvoker;

        SetCursorState(true);
        Debug.Log(
            $"InputListener Initialised: PlayerControls={playerControls != null}, Camera={playerCamera != null}, PlayerInvoker={playerInvoker != null}");
        _playerControls.Enable();
    }

    private void OnDestroy()
    {
        _playerControls.Disable();
    }

    private void FixedUpdate()
    {
        ReadMoveInput();
    }

    private void LateUpdate()
    {
        ReadCameraMoveInput();
    }

    private void ReadMoveInput()
    {
        PlayerMovement = _playerControls.Player.Move.ReadValue<Vector2>();

        Debug.Log($"Move Values Readed: {PlayerMovement}");
        
        OnMoveInput?.Invoke(PlayerMovement);
    }
    
    public void ReadCameraMoveInput()
    {
        var lookInput = _playerControls.Player.Look.ReadValue<Vector2>();
        VerticalCameraMove = lookInput.y * MouseSensY;
        HorizontalCameraMove = lookInput.x * MouseSensX;

        Debug.Log($"Camera Move Values Readed: {lookInput} => {HorizontalCameraMove}, {VerticalCameraMove}");

        OnCameraLookInput?.Invoke(VerticalCameraMove, HorizontalCameraMove);
    }

    private static void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !newState;
    }
}