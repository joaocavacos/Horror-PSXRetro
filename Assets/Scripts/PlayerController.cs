using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private Vector3 playerVelocity;
    private Vector2 moveInput;
    private Rigidbody rb;
    public bool isSprinting;

    [Header("Stamina")]
    //[SerializeField] private Slider staminaSlider;
    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaLoss;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask isGround;


    void Awake()
    {
        InputManager.OnSprint += HandleSprint;
        InputManager.OnMove += OnMove;

        rb = GetComponent<Rigidbody>();

        currentStamina = maxStamina;
        //staminaSlider.maxValue = maxStamina;
        //staminaSlider.value = currentStamina;
    }

    void Update()
    {
        ControlStamina();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnMove(Vector2 move)
    {
        moveInput = move;
    }

    private void HandleSprint(bool isSprinting)
    {
        this.isSprinting = isSprinting;
    }

    private void MovePlayer()
    {
        if (isSprinting && currentStamina > 0) //Run
        {
            playerVelocity = new Vector3(moveInput.x * runSpeed, rb.velocity.y, moveInput.y * runSpeed);
            currentStamina -= staminaLoss * Time.deltaTime;
        }
        else //Walk
        {
            if (currentStamina < 100) currentStamina += staminaLoss / 2 * Time.deltaTime;
            playerVelocity = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
        }

        rb.velocity = transform.TransformDirection(playerVelocity);
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
    }

    private bool IsWalking()
    {
        if (moveInput.magnitude > 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ControlStamina()
    {
        //staminaSlider.value = currentStamina;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.red);
    }
}
