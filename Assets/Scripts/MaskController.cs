using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class MaskController : MonoBehaviour
{
    public GameObject maskPrefab;
    public Vector2 mapMinBounds = new Vector2(-10, -10);
    public Vector2 mapMaxBounds = new Vector2(10, 10);
    
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
    
    void SpawnMask()
    {
        if (spawnMasksCount >= MAX_SPAWN_MASKS) return;
        
        Vector2 randomPosition = new Vector2(
            Random.Range(mapMinBounds.x, mapMaxBounds.x),
            Random.Range(mapMinBounds.y, mapMaxBounds.y)
        );
        
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


