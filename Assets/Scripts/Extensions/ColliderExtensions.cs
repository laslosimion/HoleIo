using UnityEngine;

public static class ColliderExtensions
{
    public static bool Contains(this Collider collider, Vector3 worldPosition)
    {
        Vector3 closest = collider.ClosestPoint(worldPosition);
        // Because closest=point if point is inside - not clear from docs I feel
        return closest == worldPosition;
    }
}