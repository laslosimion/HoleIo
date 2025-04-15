using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MoveSpeed = 0.1f;

    public void Move(Vector3 offset)
    {
        transform.transform.position += offset;
    }
}