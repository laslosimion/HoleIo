using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerInfo:ScriptableObject
{
    public float SpeedMultiplier = 0.035f;
    public float ScaleIncreaseDeMultiplier = 15f;
}
