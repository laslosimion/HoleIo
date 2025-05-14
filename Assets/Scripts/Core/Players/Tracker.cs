using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    [SerializeField] private Image _renderer;

    public void SetColor(Color color)
    {
        _renderer.color = color;
    }
}
