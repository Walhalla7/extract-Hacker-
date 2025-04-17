using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetectionState : EnemyBaseState
{
    readonly NavMeshAgent agent;
    readonly Vector3 startPoint;
    readonly Transform player;
    readonly float wanderRadius;
    readonly Light lightRef;

    public EnemyDetectionState(Enemy enemy, Animator animator, NavMeshAgent agent, float wanderRadius, Transform player, Light lightRef) : base(enemy, animator)
    {
        this.agent = agent;
        this.player = player;
        this.startPoint = enemy.transform.position;
        this.wanderRadius = wanderRadius;
        this.lightRef = lightRef;
    }

    public override void OnEnter()
    {
        Debug.Log("Detect");
        animator.CrossFade(WalkHash, crossFadeDuration);
        lightRef.color = Color.yellow;
    }

    public override void Update()
    {
        if (HasReachedDestination())
        {
            var randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += startPoint;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            var finalPosition = hit.position;

            agent.SetDestination(finalPosition);
        }
    }

    bool HasReachedDestination()
    {
        return !agent.pathPending
               && agent.remainingDistance <= agent.stoppingDistance
               && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

}
