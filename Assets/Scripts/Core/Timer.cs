using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnTimePassed;

    [SerializeField] private TMP_Text _text;

    private double _seconds;

    public void Run(double seconds)
    {
        _seconds = seconds;
        UpdateTime();

        StartCoroutine(IE_Count());
    }

    public void Stop()
    {
        StopCoroutine(IE_Count());
    }

    private IEnumerator IE_Count()
    {
        while (_seconds > 0)
        {
            yield return new WaitForSeconds(1);
            _seconds--;
            UpdateTime();
        }

        OnTimePassed?.Invoke();
    }

    private void UpdateTime()
    {
        _text.text = _seconds.ToString(CultureInfo.InvariantCulture);
    }
}