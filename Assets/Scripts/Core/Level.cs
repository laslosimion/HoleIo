using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, IRuntimeInitializable
{
    [SerializeField] private LevelInfo _levelInfo;

    public LevelInfo LevelInfo => _levelInfo;
    
    public void Initialize()
    {
        
    }
}
