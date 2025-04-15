using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : State
{
    [SerializeField] private Level[] _levels;
    [SerializeField] private PlayerController _playerController;

    private int _currentLevelCounter = 0;
    
    public override void Begin()
    {
        //Instantiate(_levels[_currentLevelCounter]);
        _playerController.Initialize();
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }
}
