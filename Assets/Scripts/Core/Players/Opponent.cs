using Random = UnityEngine.Random;

public sealed class Opponent : PlayerBase
{
    private const float MinTimeToChangeDirection = 5;
    private const float MaxTimeToChangeDirection = 10;
    
    /// <summary>
    /// ToDo: Improve system design by better encapsulating player base.
    /// </summary>
    public void AutoMove()
    {
        xMoveSpeed = -Random.Range(-1f, 1f) * _playerInfo.SpeedMultiplier;
        zMoveSpeed = -Random.Range(-1f, 1f) * _playerInfo.SpeedMultiplier;

        Invoke(nameof(AutoMove), Random.Range(MinTimeToChangeDirection, MaxTimeToChangeDirection));
    }

    public override void StopMoving()
    {
        base.StopMoving();

        CancelInvoke(nameof(AutoMove));
    }
}
