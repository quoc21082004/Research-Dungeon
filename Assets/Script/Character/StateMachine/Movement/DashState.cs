using UnityEngine;
using UnityEngine.InputSystem;

public class DashState : BaseStateMachine
{
    protected float dashSpeed = 3f;
    protected float dashLimitCD = 2.5f;
    private float dashToSprintTime = 0.3f;
    public DashState(PlayerCTL _player, PlayerStateMachine _stateMachine, string _animboolName) : base(_player, _stateMachine, _animboolName)
    {

    }
    #region IState Method
    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
        StartAnimation(player.animData.dashParameterHash);
        speedModifier = player.playerdata.basicMovement.GetDashSpeed() * dashSpeed;
        SetDashEffect();
        player.dashEffect.gameObject.SetActive(true);
        StartDash();
        DashCoolDown();
    }
    public override void Exit()
    {
        base.Exit();
        player.dashEffect.gameObject.SetActive(false);
        StopAnimation(player.animData.dashParameterHash);
    }
    public override void Execute()
    {
        base.Execute();
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
    private void StartDash()
    {
        if (movementInput != Vector2.zero)
            return;
        player.dashEffect.gameObject.SetActive(true);
        Vector2 dashDir = new Vector2(movementInput.x, movementInput.y);
        player.myrigid.velocity = (dashDir * speedModifier * player.transform.right);
    }
    private void SetDashEffect()
    {
        if (!player.mySR.flipX)
        {
            player.dashEffect.transform.position = new Vector2(player.transform.position.x - 0.858f, player.transform.position.y - 0.372f);
            player.dashEffect.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            player.dashEffect.transform.position = new Vector2(player.transform.position.x + 0.88f, player.transform.position.y - 0.372f);
            player.dashEffect.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private void DashCoolDown() => InputManager.instance.DisableActionFor(InputManager.playerInput.Player.Dash, dashLimitCD);
    #endregion
}