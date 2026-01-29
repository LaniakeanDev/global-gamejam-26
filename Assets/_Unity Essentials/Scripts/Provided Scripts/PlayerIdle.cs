using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdle : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    
    public InputActionReference moveAction;
    // Private variables 
    private Rigidbody2D body; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    public SpriteRenderer spriteRenderer;

    public Animator anim;
    private bool isFacingRight = true;
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
                    isFacingRight = true;
                else if (horizontalInput < 0)
                    isFacingRight = false;
            // Optionally rotate the player based on movement direction
        }
        else
        {
            // Determine the priority of movement based on input
            if (horizontalInput != 0)
            {
                isMovingHorizontally = true;
                if (horizontalInput > 0)
                    isFacingRight = true;
                else if (horizontalInput < 0)
                    isFacingRight = false;
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
        anim.SetFloat("horizontal", horizontalInput);
        anim.SetFloat("vertical", verticalInput);
        spriteRenderer.flipX = !isFacingRight;
    }

   void FixedUpdate()
{
    body.linearVelocity = movement * speed;
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MoveTowardPlayer>())
            anim.SetBool("isHurt", true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<MoveTowardPlayer>())
            anim.SetBool("isHurt", false);
    }


    private void OnDisable()
    {
        moveAction.action.Disable();
    }
}
