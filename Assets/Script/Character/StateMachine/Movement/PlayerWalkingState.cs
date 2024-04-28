using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMovementState
{
    public PlayerWalkingState(PlayerMovementStateMachine _movementStateMachine) : base(_movementStateMachine)
    {

    }
    #region IState
    public override void Enter()
    {
        base.Enter();
        speedModifier = stateMachine.player.playerdata.basicMovement.walkSpeed;
    }

    #endregion

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        InputManager.playerInput.Player.Move.canceled += OnMovementCancel;
    }
    protected override void UnRegisterEvent()
    {
        base.UnRegisterEvent();
        InputManager.playerInput.Player.Move.canceled -= OnMovementCancel;
    }
    protected override void OnWalkToggleEvent(InputAction.CallbackContext context)
    {
        base.OnWalkToggleEvent(context);
        stateMachine.ChangeState(stateMachine.RunState);
    }
    private void OnMovementCancel(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
