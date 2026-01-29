using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionCheck : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    
    public InputActionReference moveAction;
    // Private variables 
    private Rigidbody2D body; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    private SpriteRenderer spriteRenderer;
    private int last_side = 0;

    private int hurt_frame_count = 0;
    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    void Start()
    {
        // Initialize the Rigidbody2D component
        body = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isHurt && hurt_frame_count < 400)
            hurt_frame_count++;
        else{
            hurt_frame_count = 0;
            isHurt = false;
        }
        // Get player input from keyboard or controller
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

        // Check if diagonal movement is allowed
        if (canMoveDiagonally)
        {
            // Set movement direction based on input
            movement = new Vector2(horizontalInput, verticalInput);
                if (horizontalInput > 0)
                    last_side = 0;
                else if (horizontalInput < 0)
                    last_side = 1;
            // Optionally rotate the player based on movement direction
        }
        else
        {
            // Determine the priority of movement based on input
            if (horizontalInput != 0)
            {
                isMovingHorizontally = true;
                if (horizontalInput > 0)
                    last_side = 0;
                else if (horizontalInput < 0)
                    last_side = 1;
            }
            else if (verticalInput != 0)
            {
                isMovingHorizontally = false;
            }

            // Set movement direction and optionally rotate the player
            if (isMovingHorizontally)
            {
                movement = new Vector2(horizontalInput, 0);
            }
            else
            {
                movement = new Vector2(0, verticalInput);
            }
        }
    }
    public bool isHurt = false;
    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        body.linearVelocity = movement * speed;
        spriteRenderer.enabled = isHurt;
        spriteRenderer.flipX = last_side == 1;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<MoveTowardPlayer>() != null)
        {
            isHurt = true;
        }
    }
}
