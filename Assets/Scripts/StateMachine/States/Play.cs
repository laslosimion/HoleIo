using TMPro;
using UnityEngine;

public class Play : State
{
    [SerializeField] private Level[] _levels;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _points;
    
    private int _currentLevelCounter = 0;
    private int _currentPoints;
    private Opponent[] _opponents;
    
    public override void Begin()
    {
        var level = _levels[_currentLevelCounter];
        Instantiate(level);
        
        level.Initialize();
        _playerController.Initialize();
        _playerController.Player.OnObstacleCollected +=Player_OnObstacleCollected;
        _playerController.Player.OnOpponentCollected +=Player_OnOpponentCollected;

        _opponents = FindObjectsOfType<Opponent>();
        foreach (var item in _opponents)
        {
            item.AutoMove();
        }
        
        _timer.OnTimePassed +=Timer_OnTimePassed;
        _timer.Run(level.LevelInfo.seconds);

        _currentPoints = 0;
        UpdatePointsText();
    }

    private void Player_OnOpponentCollected(Opponent opponent)
    {
        _currentPoints += opponent.Level;
    }

    private void Player_OnObstacleCollected(Obstacle obstacle)
    {
        _currentPoints += obstacle.ObstacleInfo.value;
        UpdatePointsText();

        if (_currentPoints != _levels[_currentLevelCounter].LevelInfo.pointsTarget) 
            return;
        
        End();
        StatesMachine.GoToState<WinScreen>();
    }

    private void UpdatePointsText()
    {
        _points.text = _currentPoints.ToString();
    }

    private void Timer_OnTimePassed()
    {
        End();
    }

    public override void End()
    {
        _playerController.Player.OnObstacleCollected -=Player_OnObstacleCollected;
        _playerController.Player.OnOpponentCollected -=Player_OnOpponentCollected;
        
        _timer.OnTimePassed -=Timer_OnTimePassed;
        
        foreach (var item in _opponents)
        {
            item.StopMoving();
        }
        
        _timer.Stop();
    }
}
