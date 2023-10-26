using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    private PlayerInput input;

    private InputAction flashlightToggleAction;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction collectAction;
    private InputAction grabAction;
    private InputAction pauseAction;

    public delegate void FlashlightToggleAction();
    public static event FlashlightToggleAction OnFlashlightToggle;

    public delegate void MoveAction(Vector2 move);
    public static event MoveAction OnMove;

    public delegate void SprintAction(bool isSprinting);
    public static event SprintAction OnSprint;

    public delegate void CollectAction();
    public static event CollectAction OnCollect;

    public delegate void GrabAction();
    public static event GrabAction OnGrab;

    public delegate void PauseAction();
    public static event PauseAction OnPause;

    private void Awake()
    {
        input = new PlayerInput();
        input.Enable();

        flashlightToggleAction = input.Player.Flashlight;
        moveAction = input.Player.Move;
        sprintAction = input.Player.Sprint;
        collectAction = input.Player.Interact;
        grabAction = input.Player.Grab;
        pauseAction = input.Player.Pause;

        flashlightToggleAction.performed += TogglePerformed;
        moveAction.performed += MovePerformed;
        moveAction.canceled += MoveCanceled;
        sprintAction.performed += SprintPerformed;
        sprintAction.canceled += SprintCanceled;
        collectAction.performed += CollectPerformed;
        grabAction.performed += GrabPerformed;
        pauseAction.performed += PausePerformed;
    }

    private void OnDestroy()
    {
        input.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        OnMove?.Invoke(moveInput);
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        Vector2 moveInput = Vector2.zero;
        OnMove?.Invoke(moveInput);
    }

    private void TogglePerformed(InputAction.CallbackContext context) => OnFlashlightToggle?.Invoke();
    private void SprintPerformed(InputAction.CallbackContext context) => OnSprint?.Invoke(true);
    private void SprintCanceled(InputAction.CallbackContext context) => OnSprint?.Invoke(false);
    private void CollectPerformed(InputAction.CallbackContext context) => OnCollect?.Invoke();
    private void GrabPerformed(InputAction.CallbackContext context) => OnGrab?.Invoke();
    private void PausePerformed(InputAction.CallbackContext context) => OnPause?.Invoke();
}
