using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDetectionStrategy : IDetectionStrategy
{
    readonly float detectionAngle;
    readonly float detectionRadius;
    readonly float innerDetectionRadius;

    public ConeDetectionStrategy(float detectionAngle, float detectionRadius, float innerDetectionRadius)
    {
        this.detectionAngle = detectionAngle;
        this.detectionRadius = detectionRadius;
        this.innerDetectionRadius = innerDetectionRadius;
    }

    public bool Execute(Transform player, Transform detector, CountdownTimer timer)
    {
        if (timer.IsRunning) return false;

        var directionToPlayer = player.position - detector.position;
        var distanceToPlayer = directionToPlayer.magnitude;
        var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);

        //Debug.DrawRay(detector.position, detector.forward * detectionRadius, Color.blue); // Cone forward
        //Debug.DrawLine(detector.position, player.position, Color.yellow);

        // If the player is not within the detection angle + outer radius (aka the cone in front of the enemy),
        // or is within the inner radius, return false
        if ((!(angleToPlayer < detectionAngle / 2f) || !(directionToPlayer.magnitude < detectionRadius))
            && !(directionToPlayer.magnitude < innerDetectionRadius))
            return false;

        RaycastHit hit;
        if (Physics.Raycast(detector.position, directionToPlayer.normalized, out hit, distanceToPlayer))
        {
            // Draw hit line
            //Debug.DrawLine(detector.position, hit.point, Color.red); 
            // If the ray hits something that's not the player, there's an obstruction
            if (!hit.transform.CompareTag("Player"))
            {

                return false;
            }

        }


        timer.Start();
        return true;
    }
}