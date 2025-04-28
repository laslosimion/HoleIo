using System;
using System.Collections.Generic;
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
        transform.position += new Vector3(_xMoveSpeed, 0, _zMoveSpeed);
    }

    private void OnCollisionStay(Collision other)
    {
        var distance = Vector3.Distance(transform.position, other.transform.position);
        var boxCollider = other.collider as BoxCollider;
        if (boxCollider != null && distance < boxCollider.size.x)
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

        Debug.Log($"Distance {distance} size: {boxCollider.size.x}");
    }
}