using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // variables
    [Header("Walking/Running")]
    public float walkSpeed = 5.0f;
    private float walkSpeedModifier = 0.0f;
    public float runSpeed = 7.5f;
    private bool isSprinting; // used for techincal purposes

    [Header("Jumping")]
    public float jumpForce = 4.0f;
    
    [Tooltip("T/F variable dictating whether or not the player can control his/her movement while in the air")] 
    public bool jumpMovement = false;
    private bool isJumping; // used for technical purposes

    [Header("Input")]
    public InputActionReference inputRef_Movement;
    public InputActionReference inputRef_Jump;
    public InputActionReference inputRef_Sprint;

    Vector2 movement;

    private Rigidbody rb;
    private CapsuleCollider col;


    // functions
    public void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        inputRef_Jump.action.performed += ctx => isJumping = true;
        inputRef_Jump.action.Enable();

        inputRef_Sprint.action.performed += ctx => isSprinting = true;
        inputRef_Sprint.action.canceled += ctx => isSprinting = false;
        inputRef_Sprint.action.Enable();
    }

    public void Update() {
        // detecting WASD inputs for movement
        if (!isGrounded()) {
            if (jumpMovement) {
                movement = inputRef_Movement.action.ReadValue<Vector2>();
            } else {
                // do nothing
            }
        } else {
            movement = inputRef_Movement.action.ReadValue<Vector2>();
        }


        // applying WASD inputs transform.translate to move the player
        transform.Translate(movement.x * (walkSpeed + walkSpeedModifier) * Time.deltaTime, 0, movement.y * (walkSpeed + walkSpeedModifier) * Time.deltaTime);
    }

    public void FixedUpdate() {
        // jumping
        if (isJumping && isGrounded()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        walkSpeedModifier = isSprinting ? (runSpeed - walkSpeed) : 0;
    }

    // checks to see if the player is on the ground by shooting a raycast downwards (towards the ground)
    private bool isGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }
}
