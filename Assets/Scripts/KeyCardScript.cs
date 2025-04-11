using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{

    // ============================================================== Variables =====================================================
    // Object References    
    public DoorScript doorRef;

    // ============================================================== Start =====================================================
    // Lock the door
    void Start()
    {
        doorRef.LockDoors();
    }

    // ============================================================== Interaction =====================================================
    // Unlock the door and destroy self
    public void KeyInteract()
    {
        doorRef.UnlockDoors();
        Destroy(gameObject);
    }
}
