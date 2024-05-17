public interface IState
{
    void Enter();
    void Execute();
    void Exit();
    void HandleInput();
    void PhysicUpdate();
    void OnAnimationEnterEvent();
    void OnAnimationExitEvent();
    void OnAnimationTransitionEvent();
}
