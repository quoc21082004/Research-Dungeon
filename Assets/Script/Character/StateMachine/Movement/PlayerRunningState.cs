using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovementState
{
    public PlayerRunningState(PlayerMovementStateMachine _movementStateMachine) : base(_movementStateMachine)
    {

    }

    #region IState
    public override void Enter()
    {
        base.Enter();
        speedModifier = stateMachine.player.playerdata.basicMovement.baseSpeed;
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
        stateMachine.ChangeState(stateMachine.WalkState);
    }
    private void OnMovementCancel(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
