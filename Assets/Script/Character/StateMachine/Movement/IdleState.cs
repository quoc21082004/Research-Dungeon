using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseStateMachine
{
    public IdleState(PlayerCTL _player, PlayerStateMachine _stateMachine, string _animboolName) : base(_player, _stateMachine, _animboolName)
    {
        player = _player;
    }
    #region IState Method
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animData.idleParameterHash);
        speedModifier = 0;
        currentJumpForce = player.playerdata.basicMovement.jumpData.standardForce;
        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(player.animData.idleParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void Update()
    {
        base.Update();
        if (movementInput == Vector2.zero)
            return;
        OnMove();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        ResetVelocity();
    }
    #endregion
}
