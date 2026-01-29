using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour
{
    public Transform target;
    private SpriteRenderer spriteRenderer;
    public float speed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (target != null)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        spriteRenderer.flipX = transform.position.x - target.position.x > 0;
    }
}