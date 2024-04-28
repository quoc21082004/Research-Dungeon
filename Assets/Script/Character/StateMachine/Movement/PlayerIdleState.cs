using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerMovementState
{
    public PlayerIdleState(PlayerMovementStateMachine _movementStateMachine) : base(_movementStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        speedModifier = 0;
        ResetVelocity();
    }
    public override void Update()
    {
        base.Update();
        if (movementInput == Vector2.zero)
            return;
        OnMove();
    }
    private void OnMove()
    {
        if (shouldWalk)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }
        stateMachine.ChangeState(stateMachine.RunState);
    }
}
