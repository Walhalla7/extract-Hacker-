using System.Collections;
using KBCore.Refs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetector))]

public class Enemy : MonoBehaviour
{
    [SerializeField, Self] NavMeshAgent agent;
    [SerializeField, Self] PlayerDetector playerDetector;
    [SerializeField, Child] Animator animator;
    [SerializeField, Child] Light lightRef;
    [SerializeField] float wanderRadius = 15f;


    StateMachine stateMachine;


    void OnValidate() => this.ValidateRefs();

    void Start()
    {
        stateMachine = new StateMachine();

        var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius, lightRef);
        var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.Player, lightRef);

        At(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
        At(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.SetState(wanderState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
