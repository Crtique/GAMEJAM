using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- Declare Variables --- //
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("Ground Setting & Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    // Hard set and only changeable in the code variables
    private float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded;

    [Header("Refrences")]
    [SerializeField] private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for if we are grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        PlayerMoveInputs();
    }

    void PlayerMoveInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // Handle Gravity control
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
