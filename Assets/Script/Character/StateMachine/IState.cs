using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
    public void HandleInput();
    public void PhysicUpdate();
    public void OnAnimationEnterEvent();
    public void OnAnimationExitEvent();
    public void OnAnimationTransitionEvent();
}
