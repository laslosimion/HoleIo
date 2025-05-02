using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private const float DisablePhysicsDelay = 5f;
    
    [SerializeField] private ObstacleInfo _obstacleInfo;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;

    public ObstacleInfo ObstacleInfo => _obstacleInfo;

    public void Fall()
    {
        _rigidbody.isKinematic = false;
    }
    
    public void DisablePhysics(bool delayed)
    {
        _boxCollider.isTrigger = true;
        
        if(delayed)
            Invoke(nameof(DisablePhysicsInternal), DisablePhysicsDelay);
        else
            DisablePhysicsInternal();
    }

    private void DisablePhysicsInternal()
    {
        _rigidbody.isKinematic = true;
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(DisablePhysicsInternal));
    }
}