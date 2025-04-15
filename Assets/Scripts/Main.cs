using UnityEngine;

public sealed class Main : MonoBehaviour
{
    public static Main Instance;

    [SerializeField] private StatesMachine _statesMachine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
