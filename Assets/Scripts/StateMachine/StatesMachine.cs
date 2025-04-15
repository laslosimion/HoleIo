using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMachine : MonoBehaviour
{
    [SerializeField] private State[] _states;
    [SerializeField] private State _defaultState;

    public State CurrentState { get; private set; }

    public virtual void Start()
    {
        if (_defaultState)
            GoToState(_defaultState);
    }

    public State GoToState<T>() where T : State
    {
        foreach (var item in _states)
        {
            if (item.GetType() == typeof(T))
            {
                return GoToState(item);
            }
        }

        Debug.LogError($"{GetType()} Could not find state {typeof(T)}!");
        return null;
    }

    public State GoToState(State state)
    {
        foreach (var item in _states)
        {
            if (item.GetType() == state.GetType())
            {
                CurrentState = item;

                item.Begin();
                return CurrentState;
            }
        }

        Debug.LogError($"{GetType()} Could not find state {state}!");
        return null;
    }

    public T GetState<T>() where T : State
    {
        foreach (var item in _states)
        {
            if (item.GetType() == typeof(T))
                return item as T;
        }

        Debug.LogError($"{GetType()} Could not find state {typeof(T)}!");
        return null;
    }
}