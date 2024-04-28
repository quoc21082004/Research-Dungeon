using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    public PlayerMovementStateMachine stateMachine;
    protected Vector2 movementInput;

    protected float speedModifier = 1f;
    protected bool shouldWalk;
    public PlayerMovementState(PlayerMovementStateMachine _movementStateMachine) => stateMachine = _movementStateMachine;

    #region Interface
    public virtual void Enter()
    {
        Debug.Log("state :" + GetType());
        RegisterEvent();
    }
    public virtual void Exit()
    {
        UnRegisterEvent();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }
    public virtual void Update()
    {
        RotateScale();
    }
    public virtual void PhysicUpdate()
    {
        Move();
    }
    #endregion

    #region Main Method
    protected void ReadMovementInput()
    {
        movementInput = InputManager.playerInput.Player.Move.ReadValue<Vector2>();
    }
    protected void Move()
    {
        if (movementInput == Vector2.zero || speedModifier == 0)
        {
            ResetVelocity();
            return;
        }
        Vector2 movementDirection = GetMovementInputDirection();
        float movementSpeed = GetMovementSpeed();
        stateMachine.player.myrigid.velocity = (movementDirection * movementSpeed * Time.fixedDeltaTime);
        RotateScale();
    }
    protected Vector2 GetMovementInputDirection()
    {
        return new Vector2(movementInput.x, movementInput.y);
    }
    protected float GetMovementSpeed()
    {
        return stateMachine.player.speed * speedModifier;
    }
    protected void RotateScale()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(stateMachine.player.transform.position);
        if (mousePos.x < playerScreenPos.x)
        {
            stateMachine.player.mySR.flipX = true;
            stateMachine.player.mousefollow.FaceMouse();
        }
        else
        {
            stateMachine.player.mySR.flipX = false;
            stateMachine.player.mousefollow.FaceMouse();
        }
    }
    protected void ResetVelocity() => stateMachine.player.myrigid.velocity = Vector2.zero;

    #endregion

    #region Input Action
    protected virtual void RegisterEvent()
    {
        InputManager.playerInput.Player.WalkToggle.started += OnWalkToggleEvent;
    }
    protected virtual void UnRegisterEvent()
    {
        InputManager.playerInput.Player.WalkToggle.started -= OnWalkToggleEvent;
    }
    protected virtual void OnWalkToggleEvent(InputAction.CallbackContext context)
    {
        shouldWalk = !shouldWalk;
    }

    #endregion
}
