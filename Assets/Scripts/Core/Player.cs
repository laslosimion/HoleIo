using UnityEngine;

public class Player : MonoBehaviour
{
    private const float SpeedDeMultiplier = 1000f;
    
    private float _xMoveSpeed;
    private float _zMoveSpeed;

    public void Move(Vector3 offset)
    {
        _xMoveSpeed = -offset.x / SpeedDeMultiplier;
        _zMoveSpeed = -offset.z / SpeedDeMultiplier;
    }

    public void StopMoving()
    {
        _xMoveSpeed = _zMoveSpeed = 0;
    }

    private void Update()
    {
        var cachedTransform = transform;
        cachedTransform.position += new Vector3(_xMoveSpeed, 0, _zMoveSpeed);
    }
}