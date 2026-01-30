using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class MaskController : MonoBehaviour
{
    public GameObject maskPrefab;
    private PlayerController playerController;
    public GameObject player;
    // float width = GameObject.Find("Map").GetComponent<SpriteRenderer>().bounds.size.x;
    // float height = GameObject.Find("Map").GetComponent<SpriteRenderer>().bounds.size.y;
    // public Vector2 mapMinBounds = new Vector2(-width, -height);
    // public Vector2 mapMaxBounds = new Vector2(width, height);
    
    private int spawnMasksCount = 0;
    private const int MAX_SPAWN_MASKS = 3;
    
    void Start()
    {
        SpawnMask();
    }
    
    void Update()
    {
        // Optional: Check if masks need respawning
    }

    Vector2 getRandomPosition()
    {
        float width = GameObject.Find("Map").GetComponent<SpriteRenderer>().bounds.size.x * 0.45f;
        float height = GameObject.Find("Map").GetComponent<SpriteRenderer>().bounds.size.y * 0.45f;
        // Transform playerTransform = GameObject.FindWithTag("Player").transform;
        // Vector3 playerPosition = playerTransform.position;
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        Vector2 playerPos2D = new Vector2(playerPosition.x, playerPosition.y);
        Vector2 randomPosition = new Vector2(
            Random.Range(-width, width),
            Random.Range(-height, height)
        );
        while (Vector2.Distance(playerPos2D, randomPosition) < 10)
        {
            randomPosition = new Vector2(
                Random.Range(-width, width),
                Random.Range(-height, height)
            );
        }
        return randomPosition;
    }
    
    void SpawnMask()
    {
        if (spawnMasksCount >= MAX_SPAWN_MASKS) return;
        
        // Vector2 randomPosition = new Vector2(
        //     Random.Range(mapMinBounds.x, mapMaxBounds.x),
        //     Random.Range(mapMinBounds.y, mapMaxBounds.y)
        // );

        Vector2 randomPosition = getRandomPosition();
        
        if (maskPrefab == null) return;
        GameObject newMask = Instantiate(maskPrefab, randomPosition, Quaternion.identity);
        MaskCollectible collectible = newMask.GetComponent<MaskCollectible>();
        if (collectible != null)
        {
            collectible.OnCollected += HandleMaskCollected;
        }
        
        spawnMasksCount++;
        // Debug.Log($"Mask spawned! Total: {currentMaskCount}");
    }
    
    void HandleMaskCollected()
    {
        SpawnMask(); // Spawn a new one when one is collected
    }
}


