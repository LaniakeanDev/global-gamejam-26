using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    public LayerMask targetLayer;
    public float checkRadius = 0.5f;

    public bool checkPosition(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, checkRadius, targetLayer);
        return colliders.Length > 0;
    }
}