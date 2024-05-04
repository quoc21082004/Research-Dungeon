using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashState : BaseStateMachine
{
    protected float dashSpeed = 20f;
    protected float timeConsective = 1f;
    protected int consectiveDashLimitAmount = 2;
    protected float dashLimitCD = 1.75f;
    private int consectiveDashUsed = 0;
    private float dashToSprintTime = 1f;
    public DashState(PlayerCTL _player, PlayerStateMachine _stateMachine, string _animboolName) : base(_player, _stateMachine, _animboolName)
    {

    }
    #region IState Method
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animData.dashParameterHash);
        startTime = Time.time;
        speedModifier = dashSpeed;
        currentJumpForce = player.playerdata.basicMovement.jumpData.hardForce;
        Dash();
        player.dustprefab.gameObject.SetActive(true);
        UpdateConsectiveDash();
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(player.animData.sprintParameterHash);
        player.dustprefab.gameObject.SetActive(false);
    }
    public override void Update()
    {
        base.Update();
        if (Time.time < startTime + dashToSprintTime)
            return;
        stateMachine.ChangeState(player.sprintState);
    }
    public override void OnAnimationTransitionEvent()
    {
        if (movementInput == Vector2.zero) 
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
        stateMachine.ChangeState(player.sprintState);
    }
    #endregion

    #region Input Method
    protected override void OnMovementCancel(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(player.idleState);
    }
    protected override void OnDashStart(InputAction.CallbackContext context)
    {

    }

    #endregion

    #region Recurb Method
    private void Dash()
    {
        if (movementInput != Vector2.zero)
            return;
        Vector3 dashDirection = player.transform.forward;
        player.myrigid.velocity = dashDirection * speedModifier;
    }
    private void UpdateConsectiveDash()
    {
        if (!IsConsective())
        {
            consectiveDashUsed = 0;
        }
        ++consectiveDashUsed;
        if (consectiveDashUsed == consectiveDashLimitAmount)
        {
            consectiveDashLimitAmount = 0;
            InputManager.instance.DisableActionFor(InputManager.playerInput.Player.Dash, dashLimitCD);
        }
    }
    private bool IsConsective()
    {
        return Time.time > startTime + timeConsective;
    }

    #endregion
}