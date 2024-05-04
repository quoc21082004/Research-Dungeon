using System;
using UnityEngine;

[System.Serializable]
public class PlayerAnimationData
{
    [Header("Animation Parameter Name")]
    [SerializeField] public string idleParameterName = "Idle";
    [SerializeField] public string moveParameterName = "Move";
    [SerializeField] public string sprintParameterName = "Sprint";
    [SerializeField] public string dashParameterName = "Dash";
    [SerializeField] public string hurtParameterName = "Hurt";
    [SerializeField] public string attackParameterName = "Attack";

    [SerializeField] public int idleParameterHash { get; private set; }
    [SerializeField] public int moveParameterHash { get; private set; }
    [SerializeField] public int sprintParameterHash { get; private set; }
    [SerializeField] public int dashParameterHash { get; private set; }
    [SerializeField] public int hurtParameterHash { get; private set; }
    [SerializeField] public int attackParameterHash { get; private set; }
    public void InitializeAnimation()
    {
        idleParameterHash = Animator.StringToHash(idleParameterName);
        moveParameterHash = Animator.StringToHash(moveParameterName);
        sprintParameterHash = Animator.StringToHash(sprintParameterName);
        dashParameterHash = Animator.StringToHash(dashParameterName);
        hurtParameterHash = Animator.StringToHash(hurtParameterName);
        attackParameterHash = Animator.StringToHash(attackParameterName);
    }
}
