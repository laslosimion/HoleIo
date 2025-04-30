using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleInfo _obstacleInfo;

    public ObstacleInfo ObstacleInfo => _obstacleInfo;
}