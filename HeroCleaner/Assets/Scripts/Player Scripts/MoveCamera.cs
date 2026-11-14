using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // --- Declare Variables --- //

    // Mouse Sensitivity
    [SerializeField] private float mouseSens = 100f;

    // Rotate Player
    [SerializeField] private Transform playerBody;
    float xRot = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // locker the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLookInputs();
    }

    // Method for checking all of the mouse inputs that would moe our camera
    void MouseLookInputs()
    {
        float mousesX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mousesY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // Rotate the Camera to look up and down on the Y axis, clamp that rotation to be only 90.0 Degrees up and down
        xRot -= mousesY;
        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);
        
        // Rotate the player on the X axis when we look left and right with our mouse
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up, mousesX);
    }
}
