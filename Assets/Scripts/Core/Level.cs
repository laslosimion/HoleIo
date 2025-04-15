using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour, IRuntimeInitializable
{
    public abstract void Initialize();
}
