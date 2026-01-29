using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform target;
    private SpriteRenderer spriteRenderer;
    public float speed;
    public enum NPCState { Idle, Chasing }
    public NPCState state;
    private Vector2 wanderTarget;
    private float wanderRadius;

    public float conviction = 2f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        // Debug.Log("NPC conviction: " + conviction);
        if (conviction <= 0)
            state = NPCState.Idle;
        if (state == NPCState.Chasing && target != null)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else
        {
            wanderTarget = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius;
            transform.position = Vector2.MoveTowards(transform.position, wanderTarget, speed * 0.5f * Time.deltaTime);
        }

        spriteRenderer.flipX = transform.position.x - target.position.x > 0;
    }

    public void addConviction(float convictionDelta)
    {
        conviction += convictionDelta;
    }
}