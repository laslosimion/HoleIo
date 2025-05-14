using System.Collections.Generic;
using UnityEngine;

public class OpponentsUITrackers : MonoBehaviour
{
    [SerializeField] private GameObject _tracker;

    private Opponent[] _opponents;

    private readonly Dictionary<Opponent, GameObject> _trackers = new();
    private Camera _camera;

    public void Refresh(Opponent[] opponents)
    {
        _opponents = opponents;
        _camera = Camera.main;
    }

    private void Update()
    {
        UpdateTrackers();

        UpdateArrows();
    }

    private void UpdateArrows()
    {
        foreach (var item in _trackers)
        {
            var screenPos = _camera.WorldToScreenPoint(item.Key.transform.position);
            var rectTransform = item.Value.GetComponent<RectTransform>();
            var arrowPos = rectTransform.position;

            var direction = screenPos - arrowPos;

            rectTransform.up = direction;
        }
    }

    private void UpdateTrackers()
    {
        foreach (var item in _opponents)
        {
            switch (item.MainRenderer.isVisible)
            {
                case false when !_trackers.ContainsKey(item):
                    CreateTracker(item);
                    break;
                case true when _trackers.ContainsKey(item):
                    RemoveTracker(item);
                    break;
            }
        }
    }

    private void CreateTracker(Opponent opponent)
    {
        var trackerInstance = Instantiate(_tracker, transform);
        var trackerComponent = trackerInstance.GetComponent<Tracker>();
        trackerComponent.SetColor(opponent.SecondaryRenderer.color);

        _trackers.Add(opponent, trackerInstance);
    }

    private void RemoveTracker(Opponent opponent)
    {
        _trackers.TryGetValue(opponent, out var trackerInstance);
        Destroy(trackerInstance);
        _trackers.Remove(opponent);
    }
}