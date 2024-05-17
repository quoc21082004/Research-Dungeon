using UnityEngine;
using UnityEngine.InputSystem;
public class BaseStateMachine : IState
{
    protected PlayerCTL player;
    protected PlayerStateMachine stateMachine;

    #region Variable Movement
    protected Vector2 movementInput;
    protected float speedModifier = 1f;
    protected float startTime;
    protected bool shouldWalk;
    #endregion

    public BaseStateMachine(PlayerCTL _player, PlayerStateMachine _stateMachine, string _animboolName)
    {
        player = _player;
        stateMachine = _stateMachine;
    }
    #region IState Method
    public virtual void Enter() => AddInputActionsCallBack();
    public virtual void Execute()
    {
        ReadMovementInput();
        FlipCharacter();
    }
    public virtual void Exit() => RemoveInputActionsCallBack();
    public virtual void HandleInput() { }
    public virtual void PhysicUpdate()
    {
        Move();
    }
    public virtual void OnAnimationEnterEvent()
    {

    }
    public virtual void OnAnimationExitEvent()
    {

    }
    public virtual void OnAnimationTransitionEvent()
    {

    }
    #endregion

    #region Input Method
    protected virtual void AddInputActionsCallBack()
    {
        InputManager.playerInput.Player.Move.started += OnMovementCancel;
        InputManager.playerInput.Player.Dash.started += OnDashStart;
        InputManager.playerInput.Player.Sprint.started += OnSprintStart;
    }
    protected virtual void RemoveInputActionsCallBack()
    {
        InputManager.playerInput.Player.Move.started -= OnMovementCancel;
        InputManager.playerInput.Player.Dash.started -= OnDashStart;
        InputManager.playerInput.Player.Sprint.started -= OnSprintStart;
    }
    protected virtual void OnMovementCancel(InputAction.CallbackContext context) => stateMachine.ChangeState(player.idleState);
    protected virtual void OnDashStart(InputAction.CallbackContext context) => stateMachine.ChangeState(player.dashState);
    protected virtual void OnSprintStart(InputAction.CallbackContext context) => stateMachine.ChangeState(player.sprintState);

    #endregion

    #region Resurb Method
    public void StartAnimation(int hashAnimation) => player.myanim.SetBool(hashAnimation, true);
    public void StopAnimation(int hasAnimation) => player.myanim.SetBool(hasAnimation, false);
    protected void ReadMovementInput() => movementInput = InputManager.playerInput.Player.Move.ReadValue<Vector2>();
    protected void Move()
    {
        if (movementInput == Vector2.zero || speedModifier == 0)
        {
            ResetVelocity();
            return;
        }
        Vector2 movementDirection = GetMovementInputDirection();
        float movementSpeed = GetMovementSpeed();
        player.myrigid.velocity = (movementDirection * movementSpeed * Time.fixedDeltaTime);
    }
    protected virtual void OnMove()
    {
        if (shouldWalk)
            return;
        stateMachine.ChangeState(player.moveState);
    }
    protected Vector2 GetMovementInputDirection()
    {
        return new Vector2(movementInput.x, movementInput.y);
    }
    protected float GetMovementSpeed()
    {
        return player.playerdata.basicMovement.GetBaseSpeed() * speedModifier;
    }
    protected void FlipCharacter()  // flip according mouse
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        if (mousePos.x < playerScreenPos.x)
        {
            
            player.mySR.flipX = true;
            player.mousefollow.FaceMouse();
        }
        else
        {
            player.mySR.flipX = false;
            player.mousefollow.FaceMouse();
        }
    }
    protected void ResetVelocity() => player.myrigid.velocity = Vector2.zero;

    #endregion
}