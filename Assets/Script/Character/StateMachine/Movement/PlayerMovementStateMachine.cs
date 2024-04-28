using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : BaseStateMachine
{
    public PlayerCTL player { get; }
    public PlayerIdleState IdleState { get; }
    public PlayerWalkingState WalkState { get; }
    public PlayerRunningState RunState { get; }

    public PlayerMovementStateMachine(PlayerCTL _player)
    {
        player = _player;
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkingState(this);
        RunState = new PlayerRunningState(this);
    }
}
