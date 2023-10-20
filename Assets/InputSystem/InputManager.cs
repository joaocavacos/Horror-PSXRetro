using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput input;

    private InputAction flashlightToggleAction;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction interactAction;
    private InputAction grabAction;
    private InputAction pauseAction;

    public delegate void FlashlightToggleAction();
    public event FlashlightToggleAction OnFlashlightToggle;

    public delegate void MoveAction(Vector2 move);
    public event MoveAction OnMove;

    public delegate void SprintAction(bool isSprinting);
    public event SprintAction OnSprint;

    public delegate void InteractAction();
    public event InteractAction OnInteract;

    public delegate void GrabAction();
    public event GrabAction OnGrab;

    public delegate void PauseAction();
    public event PauseAction OnPause;

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInput();

        flashlightToggleAction = input.Player.Flashlight;
        moveAction = input.Player.Move;
        sprintAction = input.Player.Sprint;
        interactAction = input.Player.Interact;
        grabAction = input.Player.Grab;
        pauseAction = input.Player.Pause;

        flashlightToggleAction.performed += TogglePerformed;
        moveAction.performed += MovePerformed;
        moveAction.canceled += MoveCanceled;
        sprintAction.performed += SprintPerformed;
        sprintAction.canceled += SprintCanceled;
        interactAction.performed += InteractPerformed;
        grabAction.performed += GrabPerformed;
        pauseAction.performed += PausePerformed;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
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
    private void InteractPerformed(InputAction.CallbackContext context) => OnInteract?.Invoke();
    private void GrabPerformed(InputAction.CallbackContext context) => OnGrab?.Invoke();
    private void PausePerformed(InputAction.CallbackContext context) => OnPause?.Invoke();
}
