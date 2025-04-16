using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{

    public JumpState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void OnEnter()
    {
        Debug.Log("Enter on jump");
        animator.CrossFade(JumpHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        player.HandleJump();
        player.HandleMovement();
    }
}