using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ============================================================== Variables =====================================================
    // Movement Variables
    public float moveSpeed = 6f, gravityModifier = 2f, jumpPower = 8f, runSpeed = 12f;
    public CharacterController charCon;

    private Vector3 moveInput;
    private float yVelocity;

    public Transform camTrans;
    public float mouseSensitivity = 2f;
    public bool invertX, invertY;
    public float maxLookAngle = 80f;

    // Raycast Variables    
    public float maxInteractDistance = 2.5f;

    // ============================================================== Update =====================================================
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }

        // Horizontal movement
        Vector3 vertMove = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxisRaw("Horizontal");
        moveInput = (horiMove + vertMove).normalized;

        // Running
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        moveInput *= currentSpeed;

        // Apply gravity
        if (charCon.isGrounded)
        {
            yVelocity = -1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPower;
            }
        }
        else
        {
            yVelocity += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        moveInput.y = yVelocity;
        charCon.Move(moveInput * Time.deltaTime);

        // Mouse Look
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        if (invertX) mouseInput.x = -mouseInput.x;
        if (invertY) mouseInput.y = -mouseInput.y;

        transform.Rotate(0f, mouseInput.x, 0f);

        float camRotationX = camTrans.localEulerAngles.x - mouseInput.y;
        if (camRotationX > 180f) camRotationX -= 360f;
        camRotationX = Mathf.Clamp(camRotationX, -maxLookAngle, maxLookAngle);

        camTrans.localEulerAngles = new Vector3(camRotationX, 0f, 0f);
    }

    // ============================================================== Update =====================================================
    // Send out a raycast and interact with an object in front of player
    void Interaction()
    {
        //Send out raycast
        RaycastHit hit;
        if (Physics.Raycast(camTrans.position, camTrans.TransformDirection(Vector3.forward), out hit, maxInteractDistance))
        {
            //Draw Visualization of Ray
            //Debug.DrawRay(camTrans.position, camTrans.TransformDirection(Vector3.forward) * hit.distance, Color.red, maxInteractDistance);

            // Door Interaction
            if (hit.transform.CompareTag("Doors"))
            {
                DoorScript doorRef = hit.transform.GetComponent<DoorScript>();
                if (doorRef != null)
                {
                    doorRef.DoorInteract();
                }
            }

            // Key Interact
            if (hit.transform.CompareTag("Key"))
            {
                KeyCardScript keyRef = hit.transform.GetComponent<KeyCardScript>();
                if (keyRef != null)
                {
                    keyRef.KeyInteract();
                }
            }

        }
    }

}