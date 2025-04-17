using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] float detectionAngle = 60f; // Cone in front of enemy
    [SerializeField] float detectionRadius = 10f; // Large circle around enemy
    [SerializeField] float innerDetectionRadius = 5f; // Small circle around enemy
    [SerializeField] float detectionCooldown = 1f; // Time between detections
    [SerializeField] float attackRange = 5f; // Distance from enemy to player to attack
    [SerializeField] float noticeRate = 0.1f;

    public float currentEnemyAwareness = 0f;
    public float generalEnemyAwareness = 0f;
    public float maxEnemyAwareness = 100f;
    public float globalPrisonAwareness = 0f;


    public Transform Player { get; private set; }

    CountdownTimer detectionTimer;

    IDetectionStrategy detectionStrategy;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform; // Make sure to TAG the player!
    }

    void Start()
    {
        detectionTimer = new CountdownTimer(detectionCooldown);
        detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
    }

    void Update()
    {
        detectionTimer.Tick(Time.deltaTime);
        if (CanDetectPlayer())
        {
            float currentNoticeRatio = noticeRate;
            if (CanAttackPlayer())
            {
                currentNoticeRatio = currentNoticeRatio * 5f;
            }
            float totalAwarenessGained = currentNoticeRatio + generalEnemyAwareness + globalPrisonAwareness;

            if (currentEnemyAwareness != maxEnemyAwareness && currentEnemyAwareness + totalAwarenessGained >= maxEnemyAwareness)
            {
                generalEnemyAwareness += 0.5f;
            }

            currentEnemyAwareness = Mathf.Min(currentEnemyAwareness + totalAwarenessGained, maxEnemyAwareness);
            Debug.Log("currentNoticeRatio: " + currentNoticeRatio);
            Debug.Log("generalEnemyAwareness: " + generalEnemyAwareness);
            Debug.Log("globalPrisonAwareness: " + globalPrisonAwareness);
        }
        else
        {
            currentEnemyAwareness = Mathf.Max(currentEnemyAwareness - noticeRate, 0);
            //Debug.Log(currentEnemyAwareness);
        }
    }

    public bool CanDetectPlayer()
    {
        return detectionTimer.IsRunning || detectionStrategy.Execute(Player, transform, detectionTimer);
    }

    public bool CanAttackPlayer()
    {
        var directionToPlayer = Player.position - transform.position;
        return directionToPlayer.magnitude <= attackRange;
    }

    public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => this.detectionStrategy = detectionStrategy;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw a spheres for the radii
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);

        // Calculate our cone directions
        Vector3 forwardConeDirection = Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRadius;
        Vector3 backwardConeDirection = Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRadius;

        // Draw lines to represent the cone
        Gizmos.DrawLine(transform.position, transform.position + forwardConeDirection);
        Gizmos.DrawLine(transform.position, transform.position + backwardConeDirection);
    }
}