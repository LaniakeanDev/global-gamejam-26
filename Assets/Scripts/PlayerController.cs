using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float SPEED = 5f; // The SPEED at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally

    public int conviction = 50;
    private float contactTimer;
    private float CONTACT_TIMER_FREQ = 0.5f;
    public int CONVICTION_GAIN = 1;
    public int PLAYER_CONVICTION_LOSS = 1;

    private int CONVERSION_SCORE_GAIN = 1;
    private int MASK_SCORE_GAIN = 10;

    public int score = 0;

    
    public InputActionReference moveAction;
    // Private variables 
    private Rigidbody2D body; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    private GameManager gameManager;

    public SpriteRenderer spriteRenderer;

    public Animator anim;
    private bool isFacingRight = true;
    private bool alreadyDead = false;
    private Animator animator;
    private bool IsDead
    {
        get
        {
            if (animator == null) return false;
            return animator.GetCurrentAnimatorStateInfo(0).IsName("die");
        }
    }

    public int collectedMasks;


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
        // Debug.Log("conviction= " + conviction );
        animator = GetComponent<Animator>();
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(2, 0);
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (IsDead && !alreadyDead)
        {
            alreadyDead = true;
            GameManager.instance.die();
        }
            

        anim.SetFloat("conviction", conviction);
        if (conviction <= 0)
        {
            body.constraints |= RigidbodyConstraints2D.FreezePositionX;
            body.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
        else if (conviction > 100)
            conviction = 100;
            
        // Get player input from keyboard or controller
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

        // Check if diagonal movement is allowed
        if (canMoveDiagonally && !IsDead)
        {
            // Set movement direction based on input
            movement = new Vector2(horizontalInput, verticalInput);
                if (horizontalInput > 0)
                    isFacingRight = true;
                else if (horizontalInput < 0)
                    isFacingRight = false;
            // Optionally rotate the player based on movement direction
        }
        else if (!IsDead)
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
        if (conviction >= 3)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(2, 1);
        }
        else if (conviction >= 2)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(2, 0);
        }
        spriteRenderer.flipX = !isFacingRight;
    }

    void FixedUpdate()
    {
        body.linearVelocity = movement * SPEED;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        NPCController npc = other.GetComponent<NPCController>();
        // Debug.Log("conviction= " + conviction + ", npc.conviction= " + npc.conviction);
        if (npc && conviction < npc.conviction)
        {
            anim.SetBool("isHurt", true);
            conviction -= PLAYER_CONVICTION_LOSS;
            contactTimer = 0f; // Reset timer on initial contact
        }
        else if (npc && npc.conviction > 0 && conviction >= npc.conviction)
        {
            npc.conviction -= 1;
            contactTimer = 0f;
            conviction += CONVICTION_GAIN;
            score += CONVERSION_SCORE_GAIN;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        NPCController npc = other.GetComponent<NPCController>();
        contactTimer += Time.deltaTime;
        // Debug.Log("conviction= " + conviction + ", npc.conviction= " + npc.conviction);
        if (npc && conviction < npc.conviction)
        {
            
            if (contactTimer >= CONTACT_TIMER_FREQ)
            {
                // Apply continuous damage/effect
                conviction -= PLAYER_CONVICTION_LOSS;                
                contactTimer = 0f; // Reset for next interval
            }
        }
        else if (npc && npc.conviction > 0 && conviction >= npc.conviction)
        {
            if (contactTimer >= 0.5f)
            {
                contactTimer = 0f;
                npc.conviction -= 1;
                conviction += CONVICTION_GAIN;
                score += CONVERSION_SCORE_GAIN;
            }
        }
    }

    public void maskScoreIncrease()
    {
        score += MASK_SCORE_GAIN;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPCController>())
        {
            contactTimer = 0f;
            anim.SetBool("isHurt", false);
        }
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

}
