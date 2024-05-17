using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : BaseStateMachine
{
    private PlayerSO playerdata;
    protected bool keepSprint;
    [SerializeField][Range(1f, 5f)] protected float sprintToRunTime = 1f;
    public SprintState(PlayerCTL _player, PlayerStateMachine _stateMachine, string _animboolName) : base(_player, _stateMachine, _animboolName)
    {
        playerdata = _player.playerdata;
    }
    #region IState Method
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animData.sprintParameterHash);
        speedModifier = playerdata.basicMovement.GetRunSpeed();
        startTime = Time.time;
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(player.animData.sprintParameterHash);
        keepSprint = false;
    }
    public override void Execute()
    {
        base.Execute();
        if (keepSprint)
            return;
        if (Time.time < startTime + sprintToRunTime)
            return;
        StopSprinting();
    }
    #endregion

    #region Input Method
    protected override void AddInputActionsCallBack()
    {
        base.AddInputActionsCallBack();
        InputManager.playerInput.Player.Sprint.performed += OnSprintperformed;
    }
    protected override void RemoveInputActionsCallBack()
    {
        base.RemoveInputActionsCallBack();
        InputManager.playerInput.Player.Sprint.performed -= OnSprintperformed;
    }

    private void OnSprintperformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        keepSprint = true;
    }

    #endregion

    #region Resub Method
    protected void StopSprinting()
    {
        if (movementInput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
        stateMachine.ChangeState(player.moveState);
    }

    #endregion
}