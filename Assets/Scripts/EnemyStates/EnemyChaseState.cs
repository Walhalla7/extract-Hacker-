using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    readonly NavMeshAgent agent;
    readonly Transform player;
    readonly Light lightRef;

    public EnemyChaseState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player, Light lightRef) : base(enemy, animator)
    {
        this.agent = agent;
        this.player = player;
        this.lightRef = lightRef;
    }

    public override void OnEnter()
    {
        Debug.Log("Chase");
        animator.CrossFade(RunHash, crossFadeDuration);
        lightRef.color = Color.red;
        agent.speed *= 2;
        agent.angularSpeed *= 2;
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
    }

    public override void OnExit()
    {
        agent.speed /= 2;
        agent.angularSpeed /= 2;
    }
}
