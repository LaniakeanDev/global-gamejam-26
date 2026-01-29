using UnityEngine;

// Separate script for the mask collectible
public class MaskCollectible : MonoBehaviour
{
    public System.Action OnCollected;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
