using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelInfo : ScriptableObject
{
   public int pointsTarget;
   public int seconds;
}
