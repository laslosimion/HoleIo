using System;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    private const int LevelIncreaseObstaclesRequirement = 3;
    
    public event Action<Obstacle> OnObstacleCollected;
    public event Action<Opponent> OnOpponentCollected; 

    [SerializeField] protected PlayerInfo _playerInfo;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    
    public int Level { get; private set; }
    
    protected float xMoveSpeed;
    protected float zMoveSpeed;

    private int _obstaclesCounter;

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
        if (!otherCollider) 
            return;
        
        var otherTransform = otherCollider.transform;
        var min = otherCollider.center - otherCollider.size * 0.5f;
        var max = otherCollider.center + otherCollider.size * 0.5f;
        var leftBottomBackPoint = otherTransform.TransformPoint(new Vector3(min.x, min.y, min.z));
        var rightBottomFrontPoint = otherTransform.TransformPoint(new Vector3(max.x, min.y, max.z));
        // var P001 = otherTransform.TransformPoint(new Vector3(min.x, min.y, max.z));
        // var P010 = otherTransform.TransformPoint(new Vector3(min.x, max.y, min.z));
        // var P011 = otherTransform.TransformPoint(new Vector3(min.x, max.y, max.z));
        // var P100 = otherTransform.TransformPoint(new Vector3(max.x, min.y, min.z));

        // var P110 = otherTransform.TransformPoint(new Vector3(max.x, max.y, min.z));
        // var P111 = otherTransform.TransformPoint(new Vector3(max.x, max.y, max.z));

        var opponent = other.gameObject.GetComponent<Opponent>();
        if (opponent)
        {
            if (_boxCollider.Contains(leftBottomBackPoint) || _boxCollider.Contains(rightBottomFrontPoint))
            {
                OnOpponentCollected?.Invoke(opponent);
                
                IncreaseSize(other.transform.localScale.x, otherTransform.localScale.z);
                Level++;
                other.gameObject.SetActive(false);
                return;
            }
        }

        var obstacle = other.gameObject.GetComponentInParent<Obstacle>();
        if (!obstacle)
            return;
            
        if (!_boxCollider.Contains(leftBottomBackPoint) || !_boxCollider.Contains(rightBottomFrontPoint))
            return;

        IncreaseSize(other.transform.localScale.x, otherTransform.localScale.z);

        _obstaclesCounter++;
        if (_obstaclesCounter % LevelIncreaseObstaclesRequirement == 0)
            Level++;
        
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