using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerContorller : MonoBehaviour
{
    #region Äæ¦ì«Å§i


    [SerializeField] private Transform camTransform;
    [SerializeField] private float rotateSpeed=100f;
    private Transform playerTransform;
    private Animator animator;

   
    [HideInInspector]
    public enum PlayerPos
    {
        Stand,
        inAir
    }
    
    [HideInInspector]
    public enum LocomotionState
    {
        Idle,
        Walk,
        Run
    }

    private PlayerPos playerPos = PlayerPos.Stand;

    private float standAction;
    private float inAirAction;

    [HideInInspector]
    public LocomotionState locomotionState = LocomotionState.Idle;

    [SerializeField]private float walkSpeed = 1.5f;
    [SerializeField]private float runSpeed = 5.5f;
    private float currentSpeed;

    private Vector2 moveInput;
    private bool isRunning;
    private bool isJumping;

    private int MoveSpeedHash;
    private int TunSpeedHash;
    private int PlayerActionHash;

    private Vector3 playerMovement = Vector3.zero;

    #endregion

    private void Start()
    {
        playerTransform = transform;
        
        animator = GetComponent<Animator>();
        MoveSpeedHash = Animator.StringToHash("Move Speed");
        TunSpeedHash = Animator.StringToHash("Turn Speed");
        PlayerActionHash = Animator.StringToHash("Player Action");

    }
    private void Update()
    {
        SwitchPlayerState();
        CalculateInputDirection();
        SetAnimator();
    }


    #region GetInput
    public void GetMoveInput(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        //Debug.Log(moveInput);
    }
    public void GetRunInput(InputAction.CallbackContext ctx)
    {
        isRunning = ctx.ReadValueAsButton();
    }
    public void GetJumpInput(InputAction.CallbackContext ctx)
    {
        isJumping = ctx.ReadValueAsButton();
    }
    #endregion

    private void SwitchPlayerState()
    {

        if (moveInput.magnitude == 0)
        {
            locomotionState = LocomotionState.Idle;
        }
        else if (isRunning)
        {
            locomotionState = LocomotionState.Run;
        }
        else
        {
            locomotionState = LocomotionState.Walk;
        }
    }

    private void CalculateInputDirection()
    {
        Vector3 camForWardPojection = new Vector3(camTransform.forward.x,0,camTransform.forward.z).normalized;
        playerMovement = (camForWardPojection * moveInput.y) + (camForWardPojection * moveInput.x);
        playerMovement = playerTransform.InverseTransformVector(playerMovement);
    }

    private void SetAnimator()
    {
        switch (locomotionState)
        {
            case LocomotionState.Idle:
                animator.SetFloat(MoveSpeedHash, 0,0.1f,Time.deltaTime);
                break;
            case LocomotionState.Walk:
                animator.SetFloat(MoveSpeedHash,playerMovement.magnitude*walkSpeed,0.1f,Time.deltaTime);
                break;
            case LocomotionState.Run:
                animator.SetFloat(MoveSpeedHash, playerMovement.magnitude * runSpeed, 0.1f, Time.deltaTime);
                break;
        }

        float red = Mathf.Atan2(playerMovement.x,playerMovement.z);
        animator.SetFloat(TunSpeedHash, red, 0.1f, Time.deltaTime);
        playerTransform.Rotate(0, red * rotateSpeed * Time.deltaTime, 0f);

    }


}
