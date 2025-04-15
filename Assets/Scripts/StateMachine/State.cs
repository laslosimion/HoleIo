using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private StatesMachine _statesMachine;

    public StatesMachine StatesMachine => _statesMachine;
    
    public abstract void Begin();

    public abstract void End();
}
