using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : BaseStateMachine
{
    [SerializeField][Range(0f, 2f)] protected float runToWalkTime = 0.5f;
    public MoveState(PlayerCTL _player, PlayerStateMachine _stateMachine, string _animboolName) : base(_player, _stateMachine, _animboolName)
    {

    }
    #region IState Method
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animData.moveParameterHash);
        speedModifier = player.playerdata.basicMovement.GetBaseSpeed();
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(player.animData.moveParameterHash);
    }
    public override void Update()
    {
        base.Update();
        if (!shouldWalk)
            return;
        if (Time.time < startTime + runToWalkTime)
            return;
        StopRunning();
    }
    #endregion

    #region Input Method
    protected override void AddInputActionsCallBack()
    {
        base.AddInputActionsCallBack();
        InputManager.playerInput.Player.Move.canceled += OnMovementCancel;
    }
    protected override void RemoveInputActionsCallBack()
    {
        base.RemoveInputActionsCallBack();
        InputManager.playerInput.Player.Move.canceled -= OnMovementCancel;
    }
    protected override void OnMovementCancel(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(player.idleState);
    }
    #endregion

    #region Main Method
    protected void StopRunning()
    {
        if (movementInput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
    #endregion
}
