using System;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public event Action<Obstacle> OnObstacleCollected;

    [SerializeField] protected PlayerInfo _playerInfo;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    
    protected float xMoveSpeed;
    protected float zMoveSpeed;

    public void Move(Vector3 offset)
    {
        _rigidbody.isKinematic = false;
        
        xMoveSpeed = -offset.x * _playerInfo.SpeedMultiplier;
        zMoveSpeed = -offset.z * _playerInfo.SpeedMultiplier;
    }

    public virtual void StopMoving()
    {
        xMoveSpeed = zMoveSpeed = 0;
        
        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        transform.position += new Vector3(xMoveSpeed * Time.deltaTime, 0, zMoveSpeed* Time.deltaTime);
    }

    private void OnCollisionStay(Collision other)
    {
        var otherCollider = other.collider as BoxCollider;

        if (otherCollider != null)
        {
            var trans = otherCollider.transform;
            var min = otherCollider.center - otherCollider.size * 0.5f;
            var max = otherCollider.center + otherCollider.size * 0.5f;

            var leftBottomBackPoint = trans.TransformPoint(new Vector3(min.x, min.y, min.z));
            // var P001 = trans.TransformPoint(new Vector3(min.x, min.y, max.z));
            // var P010 = trans.TransformPoint(new Vector3(min.x, max.y, min.z));
            // var P011 = trans.TransformPoint(new Vector3(min.x, max.y, max.z));
            // var P100 = trans.TransformPoint(new Vector3(max.x, min.y, min.z));
            var rightBottomFrontPoint = trans.TransformPoint(new Vector3(max.x, min.y, max.z));
            // var P110 = trans.TransformPoint(new Vector3(max.x, max.y, min.z));
            // var P111 = trans.TransformPoint(new Vector3(max.x, max.y, max.z));

            if (!_boxCollider.Contains(leftBottomBackPoint) || !_boxCollider.Contains(rightBottomFrontPoint))
                return;
        }
        else
            return;

        var otherPlayer = other.gameObject.GetComponent<PlayerBase>();
        if (!otherPlayer)
            IncreaseSize(other.transform.localScale.x, other.transform.localScale.z);

        var obstacle = other.gameObject.GetComponentInParent<Obstacle>();
        if (!obstacle) return;
        
        obstacle.Fall();
        obstacle.DisablePhysics(true);

        OnObstacleCollected?.Invoke(obstacle);
    }

    private void IncreaseSize(float x, float y)
    {
        var localScale = transform.localScale;
        localScale = new Vector3(localScale.x + x / _playerInfo.ScaleIncreaseDeMultiplier, localScale.y + y / _playerInfo.ScaleIncreaseDeMultiplier, localScale.z);
        
        transform.localScale = localScale;
    }
}