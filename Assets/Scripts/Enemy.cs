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
    public GameObject LoseCanvas;


    StateMachine stateMachine;


    void OnValidate() => this.ValidateRefs();

    void Start()
    {
        stateMachine = new StateMachine();

        var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius, lightRef);
        var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.Player, lightRef);
        var detectionState = new EnemyDetectionState(this, animator, agent, wanderRadius, playerDetector.Player, lightRef);

        At(wanderState, detectionState, new FuncPredicate(() => playerDetector.currentEnemyAwareness > 0));
        At(detectionState, wanderState, new FuncPredicate(() => playerDetector.currentEnemyAwareness == 0));
        At(detectionState, chaseState, new FuncPredicate(() => playerDetector.currentEnemyAwareness >= playerDetector.maxEnemyAwareness * 0.7));
        At(chaseState, detectionState, new FuncPredicate(() => playerDetector.currentEnemyAwareness < playerDetector.maxEnemyAwareness * 0.7));
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("death");
            LoseCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
