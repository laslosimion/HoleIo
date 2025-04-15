using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : State
{
    [SerializeField] private Level[] _levels;

    private int _currentLevelCounter = 0;
    
    public override void Begin()
    {
        Instantiate(_levels[_currentLevelCounter]);
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }
}
