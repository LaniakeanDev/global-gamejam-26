using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform target;
    private SpriteRenderer spriteRenderer;
    public float speed;
    public enum NPCState { Idle, Chasing }
    public NPCState state;
    private Vector2 wanderTarget;
    private float wanderRadius = 500;
    private float wanderTimer = 0f;
    private const float WANDER_UPDATE_INTERVAL = 5f;

    public float conviction = 5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (conviction <= 0)
        {
            state = NPCState.Idle;
            conviction = 0;
        }
        if (state == NPCState.Chasing && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            spriteRenderer.flipX = transform.position.x - target.position.x > 0;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, wanderTarget, speed * 0.5f * Time.deltaTime);
            spriteRenderer.flipX = transform.position.x - wanderTarget.x > 0;
        }

    }


    void FixedUpdate()
    {
        wanderTimer += Time.fixedDeltaTime;
        
        if (wanderTimer >= WANDER_UPDATE_INTERVAL)
        {
            wanderTarget = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius;
            wanderTimer = 0f; // Reset timer
        }
    }
}