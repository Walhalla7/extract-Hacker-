using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    // ============================================================== Variables =====================================================
    // Object References
    public EnemyManager enemyManagerRef;
    public CharacterController charCon; 
    public Transform recallTracker; 

    // Energy Variables
    private int energy = 10;
    private float energyRegenTimer = 0f;
    public float energyRegenInterval = 10f; 
    private const int maxEnergy = 10;

    // X-Ray Variables
    private bool isXRayActive = false;
    private float xRayTimer = 0f;
    public const float xRayDuration = 5f;

    // Recall Variables
    private Vector3[] previousPositions = new Vector3[5];
    private float recallTimer = 0f;
    private const float recallInterval = 1f;


    // Smoke Granade Variables
    public GameObject cameraRef;
    public float throwSpeed = 5f;
    public GameObject granadePrefab; 
    
    // ============================================================== Start =====================================================
    void Start()
    {
        for (int i = 0; i < previousPositions.Length; i++)
        {
            previousPositions[i] = transform.position;
        }
    }

    // ============================================================== Update =====================================================
    void Update()
    {
        HandleAbilityUsage();
        RegenerateEnergy();
        ManageXRayTimer();
        ManageRecallTimer();
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
            // Instantiate the Granade Object
            GameObject grenade = Instantiate(granadePrefab, cameraRef.transform.position + cameraRef.transform.forward, Quaternion.identity);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            // Apply the velocity to the Granade 
            if (rb != null)
            {
                rb.velocity = cameraRef.transform.forward * throwSpeed;
            }       
            // Subtract Energy     
            energy -= 3;
            Debug.Log(energy);
        }
        // Activate Recall
        if (Input.GetKeyDown(KeyCode.Alpha3) && energy > 4)
        {
            Debug.Log("Recalling to position: " + previousPositions[4]);

            // Disable character controller and modify the postiion then re-enable controller
            if (charCon != null)
            {
                charCon.enabled = false; 
            }

            transform.position = previousPositions[4]; 

            if (charCon != null) 
            {
                charCon.enabled = true;
            }

            // Subtract Energy
            energy -= 5;
            Debug.Log(energy);
        }
    }

    // ============================================================== Energy Functions =====================================================
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

    // ============================================================== X-Ray Function =====================================================
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

    // ============================================================== Recall Function =====================================================
    // Timer logic which modifies the recall position every second 
    void ManageRecallTimer()
    {
        recallTimer += Time.deltaTime;
        if (recallTimer >= recallInterval)
        {
            for (int i = previousPositions.Length - 1; i > 0; i--)
            {
                previousPositions[i] = previousPositions[i - 1];
            }
            previousPositions[0] = transform.position;
            // Move the tracker to show where the recall will take player
            recallTracker.position = previousPositions[4];
            recallTimer = 0f;
        }
    }
}
