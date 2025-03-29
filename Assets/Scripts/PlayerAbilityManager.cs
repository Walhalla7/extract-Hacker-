using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    // ============================================================== Variables =====================================================
    // Object References
    public EnemyManager enemyManagerRef;

    // Energy Variables
    private int energy = 10;
    private float energyRegenTimer = 0f;
    public const float energyRegenInterval = 10f; 
    private const int maxEnergy = 10;

    // X-Ray Variables
    private bool isXRayActive = false;
    private float xRayTimer = 0f;
    public const float xRayDuration = 5f;

    // ============================================================== Update =====================================================
    void Update()
    {
        HandleAbilityUsage();
        RegenerateEnergy();
        ManageXRayTimer();
    }

    // ============================================================== Input For Abilities =====================================================

    void HandleAbilityUsage()
    {
        // Activate X-Ray Logic
        if (Input.GetKeyDown(KeyCode.Alpha1) && energy > 1 && !isXRayActive)
        {
            enemyManagerRef.activateXRay();
            energy -= 2;
            isXRayActive = true;
            xRayTimer = xRayDuration;
            Debug.Log(energy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && energy > 2)
        {
            //  Throw Smoke Granade
            energy -= 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && energy > 4)
        {
            //  Recall
            energy -= 5;
        }
    }

    // ============================================================== Variables =====================================================
    // Timer Logic which regenerates energy when energy is below it's maximum
    void RegenerateEnergy()
    {
        if (energy < maxEnergy)
        {
            energyRegenTimer += Time.deltaTime;
            if (energyRegenTimer >= energyRegenInterval)
            {
                energy++;
                energyRegenTimer = 0f;
                Debug.Log(energy);
            }
        }
    }

    // ============================================================== Variables =====================================================
    // Timer logic for the X-Ray to de-activate itself after 5 sec
     void ManageXRayTimer()
    {
        if (isXRayActive)
        {
            xRayTimer -= Time.deltaTime;
            if (xRayTimer <= 0f)
            {
                enemyManagerRef.activateXRay(); 
                isXRayActive = false;
            }
        }
    }
}
