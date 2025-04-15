using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler,
    IRuntimeInitializable
{
    private const float MinMovementForTrigger = 10f;

    private Player _player;
    private Vector2 _middlePosition;
    private bool _isPointerDown;

    public void Initialize()
    {
        _middlePosition = transform.position;
        _player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!_isPointerDown)
            return;

        var yDifference = _middlePosition.y - eventData.position.y;
        var moveOnY = Mathf.Abs(yDifference) > MinMovementForTrigger;
        var rectTransform = GetComponent<RectTransform>();
        var maxY = rectTransform.sizeDelta.y / 2;
        var currentYPercentage = 100 * Mathf.Abs(yDifference) / maxY;
        
        var xDifference = _middlePosition.x - eventData.position.x;
        var moveOnX = Mathf.Abs(xDifference) > MinMovementForTrigger;
        var maxX = rectTransform.sizeDelta.y / 2;
        var currentXPercentage = 100 * Mathf.Abs(xDifference) / maxY;

        if (moveOnX || moveOnY)
            _player.Move(new Vector3(xDifference, 0.0001f, yDifference));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }
}