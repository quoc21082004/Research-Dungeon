
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public IState currentState;
    public void Initialize(IState _startState)
    {
        currentState = _startState;
        currentState?.Enter();
    }
    public void ChangeState(IState _newState)
    {
        currentState?.Exit();
        currentState = _newState;
        currentState?.Enter();
    }
    public void HandleInput() => currentState?.HandleInput();
    public void Update() => currentState?.Update();
    public void PhysicUpdate() => currentState?.PhysicUpdate();
    public void OnAnimationEnterEvent() => currentState?.OnAnimationEnterEvent();
    public void OnAnimationExitEvent() => currentState?.OnAnimationExitEvent();
    public void OnAnimationTransitionEvent() => currentState?.OnAnimationTransitionEvent();

}
