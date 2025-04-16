using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("Enter on Walk");
        animator.CrossFade(LocomotionHash, crossFadeDuration);
    }
    public override void FixedUpdate()
    {
        player.HandleMovement();
    }
}

