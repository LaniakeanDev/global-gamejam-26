using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Transform target;
    private SpriteRenderer spriteRenderer;
    public int enemy_id;
    public float speed;
    public enum NPCState { Idle, Chasing }
    private NPCState state = NPCState.Chasing;
    private Vector2 wanderTarget;
    private float wanderRadius = 500;
    private float wanderTimer = 0f;
    private const float WANDER_UPDATE_INTERVAL = 5f;
    // private float INITIAL_CONVICTION = 5f;

    public float conviction;

    private float time = 0f; 
    private RandomSpawner script;

    private void Start()
    {
        target = GameObject.Find("Sprite_Relanna").GetComponent<Transform>();
        GameObject spawner = GameObject.Find("Spawners");
        script = spawner.GetComponent<RandomSpawner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (conviction <= 0)
        {
            state = NPCState.Idle;
            time = Time.time; 
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
        float distance = Mathf.Sqrt((target.position.x-transform.position.x)*(target.position.x-transform.position.x)+(target.position.y-transform.position.y)*(target.position.y-transform.position.y));
        if (time != 0)
        {
            time += Time.deltaTime;
            if (time >= 3 && distance > 10 || distance > 10){
                Destroy(gameObject);
                script.counter--;
            }
            else if (distance < 10)
                time = Time.time;
        }
        else if (distance > 15){
                Destroy(gameObject);
                script.counter--;
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


    // public void addConviction(float convictionDelta)
    // {
    //     conviction += convictionDelta;
    // }
}