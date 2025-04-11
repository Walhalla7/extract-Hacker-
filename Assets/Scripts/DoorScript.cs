using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // ============================================================== Variables =====================================================
    // State Variables
    public bool isLocked = false;
    private bool isOpen = false;

    // Object Reference Variables
    public Transform doorLeft;
    public Transform doorRight;

    // Animation Variables
    public float openDistance = 2f;
    public float openSpeed = 2f;

    // Position Variables
    private Vector3 leftClosedPos, rightClosedPos;
    private Vector3 leftOpenPos, rightOpenPos;

    // ============================================================== Start =====================================================
    void Start()
    {
        // Calculate Opened and closed positions
        leftClosedPos = doorLeft.localPosition;
        rightClosedPos = doorRight.localPosition;

        leftOpenPos = leftClosedPos + Vector3.forward * openDistance;
        rightOpenPos = rightClosedPos + Vector3.back * openDistance;
    }

    // ============================================================== Interaction =====================================================
    // Open or close the door depending on current state
    public void DoorInteract()
    {
        if (isOpen)
        {
            StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }

    // ============================================================== Opening/Closing =====================================================
    // Open the door
    private IEnumerator OpenDoor()
    {
        // Check if locked
        if (isLocked) yield break;

        isOpen = true;
        // Animate Door opening
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            doorLeft.localPosition = Vector3.Lerp(leftClosedPos, leftOpenPos, elapsed);
            doorRight.localPosition = Vector3.Lerp(rightClosedPos, rightOpenPos, elapsed);
            elapsed += Time.deltaTime * openSpeed;
            yield return null;
        }

        // Update Position
        doorLeft.localPosition = leftOpenPos;
        doorRight.localPosition = rightOpenPos;
    }

    // Close the door
    private IEnumerator CloseDoor()
    {
        //Update the state
        isOpen = false;

        // Animate Door Closing
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            doorLeft.localPosition = Vector3.Lerp(leftOpenPos, leftClosedPos, elapsed);
            doorRight.localPosition = Vector3.Lerp(rightOpenPos, rightClosedPos, elapsed);
            elapsed += Time.deltaTime * openSpeed;
            yield return null;
        }

        // Update Position
        doorLeft.localPosition = leftClosedPos;
        doorRight.localPosition = rightClosedPos;
    }

    // ============================================================== Locking/Unlocking =====================================================
    // Lock the door if it isn't currently open 
    public void LockDoors()
    {
        if (!isLocked && !isOpen)
        {
            isLocked = true;
        }
    }

    // Unlock the door
    public void UnlockDoors()
    {
        if (isLocked && !isOpen)
        {
            isLocked = false;
        }
    }
}
