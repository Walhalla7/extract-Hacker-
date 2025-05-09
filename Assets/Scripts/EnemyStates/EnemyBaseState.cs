using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : IState
{
    protected readonly Enemy enemy;
    protected readonly Animator animator;

    protected static readonly int IdleHash = Animator.StringToHash("IdleNormal");
    protected static readonly int RunHash = Animator.StringToHash("ChaseFWD");
    protected static readonly int WalkHash = Animator.StringToHash("FloatFWD");
    protected static readonly int AttackHash = Animator.StringToHash("Attack01");
    protected static readonly int DieHash = Animator.StringToHash("Die");

    protected const float crossFadeDuration = 0.1f;

    protected EnemyBaseState(Enemy enemy, Animator animator)
    {
        this.enemy = enemy;
        this.animator = animator;
    }
    public virtual void OnEnter()
    {
        //noop
    }
    public virtual void Update()
    {
        //noop

    }
    public virtual void FixedUpdate()
    {
        //noop

    }
    public virtual void OnExit()
    {
        //noop

    }

}
