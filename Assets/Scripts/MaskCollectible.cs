using UnityEngine;

public class MaskCollectible : MonoBehaviour
{
    public System.Action OnCollected;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.maskScoreIncrease();
                player.collectedMasks++;
            }
            
            Destroy(gameObject);
        }
    }
}