using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{

    // ============================================================== Variables =====================================================
    // Object References    
    public int doorAmount;
    public DoorScript doorRef;
    //public List<DoorScript> Doors;

    // ============================================================== Start =====================================================
    // Lock the door
    void Start()
    {
        /*foreach (DoorScript d in Doors)
        {
            d.LockDoors();
        }*/
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
